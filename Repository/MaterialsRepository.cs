using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Materials___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class MaterialsRepository : RepositoryBase<Materials>, IMaterialsRepository
    {
        private CruzRojaContext _cruzRojaContext;

        public MaterialsRepository(CruzRojaContext cruzRojaContext)
        : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }
        public async Task<IEnumerable<Materials>> GetAllMaterials(int userId, int locationId)
        {

            //var user =  EmployeesRepository.GetAllEmployeesById(userId);

            var collection = _cruzRojaContext.Materials as IQueryable<Materials>;

            if (!locationId.Equals(0))
                collection = collection.Where
                                            (a => a.Estates.Locations.LocationID.Equals(locationId));
            else
                return await GetAllMaterials();

            return await collection
                       .Include(a => a.Estates)
                       .Include(a => a.Estates.LocationAddress)
                       .Include(a => a.Estates.EstatesTimes)
                       .ThenInclude(a => a.Times)
                       .ThenInclude(a => a.Schedules)
                       .Include(a => a.Estates.Locations)

                        .Include(a => a.EmployeeCreated)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeCreated.Users.Roles)

                        .Include(i => i.EmployeeModified)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeModified.Users.Roles)
                       .ToListAsync();

        }



        public async Task<IEnumerable<Materials>> GetAllMaterials()
        {

            return await FindAll()
               .Include(a => a.Estates)
                       .Include(a => a.Estates.LocationAddress)
                       .Include(a => a.Estates.EstatesTimes)
                       .ThenInclude(a => a.Times)
                       .ThenInclude(a => a.Schedules)
                       .Include(a => a.Estates.Locations)

                        .Include(a => a.EmployeeCreated)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeCreated.Users.Roles)

                        .Include(i => i.EmployeeModified)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeModified.Users.Roles)
                       .ToListAsync();
        }


        public static void status(Materials materials)
        {

            CruzRojaContext cruzRojaContext = new CruzRojaContext();
            materials.Resources_RequestResources_Materials_Medicines_Vehicles = null;

            cruzRojaContext.Update(materials);

            cruzRojaContext.SaveChanges();
        }

        public async Task<Materials> GetMaterialById(string materialId)
        {
            return await FindByCondition(material => material.ID == materialId)
                           .FirstOrDefaultAsync();
        }

        public async Task<Materials> GetMaterialWithDetails(string materialId)
        {
            return await FindByCondition(material => material.ID == materialId)
                       .Include(a => a.Estates)
                       .Include(a => a.Estates.LocationAddress)
                       .Include(a => a.Estates.EstatesTimes)
                       .ThenInclude(a => a.Times)
                       .ThenInclude(a => a.Schedules)
                       .Include(a => a.Estates.Locations)

                       .Include(a => a.EmployeeCreated)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeCreated.Users.Roles)

                        .Include(i => i.EmployeeModified)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeModified.Users.Roles)
                       .FirstOrDefaultAsync();
        }

        public void CreateMaterial(Materials material, Resources_ForCreationDto resources, int userId)
        {
            //spaceCamelCase(material);
            Create(material);
            sendEmail(material, "Nuevo", userId, null, resources);
        }

        private void spaceCamelCase(Materials material)
        {
            material.MaterialName = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(material.MaterialName);
            material.MaterialBrand = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(material.MaterialBrand);
            material.MaterialUtility = WithoutSpace_CamelCase.GetWithoutSpace(material.MaterialUtility);
        }

        public void UpdateMaterial(Materials material, JsonPatchDocument<MaterialsForUpdateDto> _materials,
                                  MaterialsForUpdateDto materialToPatch, int userId)
        {
            Update(material);
            //editar id
            sendEmail(material, "Modificación de", userId,_materials, null, materialToPatch);

        }

        public void DeleteMaterial(Materials material)
        {
            Delete(material);
        }

        public IEnumerable<Materials> GetAllMaterials(DateTime dateStart, DateTime dateEnd, int estateId)
        {
            var user = EmployeesRepository.GetAllEmployeesById(estateId);

            var collection = _cruzRojaContext.Materials as IQueryable<Materials>;

            var fecha = Convert.ToDateTime("01/01/0001");

            if (dateEnd == fecha)
            {
                collection = collection.Where(
                    a => a.MaterialDateCreated >= dateStart && a.MaterialAvailability == false
                    && a.FK_EstateID == user.FK_EstateID);
            }
            else
            {
                collection = collection.Where(
                    a => a.MaterialDateCreated >= dateStart && a.MaterialDateCreated <= dateEnd
                            && a.MaterialAvailability == false
                            && a.FK_EstateID == user.FK_EstateID);
            }

            return collection
                       .Include(a => a.Estates)
                       .Include(a => a.Estates.LocationAddress)
                       .Include(a => a.Estates.EstatesTimes)
                       .ThenInclude(a => a.Times)
                       .ThenInclude(a => a.Schedules)
                       .Include(a => a.Estates.Locations)

                        .Include(a => a.EmployeeCreated)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeCreated.Users.Roles)

                        .Include(i => i.EmployeeModified)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeModified.Users.Roles)
                       .ToList();
        }



        public static void sendEmail(Materials materials, string estado, int userId,
                                     JsonPatchDocument<MaterialsForUpdateDto> _materials = null,
                                     Resources_ForCreationDto resources = null,
                                     MaterialsForUpdateDto materialToPatch = null)
        {

            string message = "";
            string messageFinal;
            CruzRojaContext cruzRojaContext1 = new CruzRojaContext();

            var user =  EmployeesRepository.GetAllEmployeesById(userId);



            var coordinadoraGeneral = cruzRojaContext1.Users.Where(x => x.FK_EstateID.Equals(user.FK_EstateID)
                                                                   && x.FK_RoleID == user.FK_RoleID)
                                                                  .Include(a => a.Persons)
                                                                  .AsNoTracking()
                                                                  .ToList();


            messageFinal = $@"

                                    <a style='color: white;
                                    text-align: center;
                                display: block;
                                    background: rgb(189, 45, 45);
                                text-decoration: none;
                                    border-radius: 0.4rem;
                                     width: 33%;
                                     margin-top: 2rem;
                                    margin-bottom: 2rem;
                                    padding: 15px; cursor: pointer; margin-left: 10rem;' href='https://calm-dune-0fef6d210.2.azurestaticapps.net/'>Ir a SICREYD</a>
                                    
        
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Este mensaje fue enviado automáticamente por el Sistema. Por favor no responda a este mensaje.
                                    </p>
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Gracias.
                                    </p>
                                    <p style='margin-left: 20px;'>
                                        El equipo de SICREYD.
                                    </p>
                            </div>

                              ";

            if (estado.Equals("Nuevo"))
            {
                var userCreate = EmployeesRepository.GetAllEmployeesById(resources.CreatedBy);

                CruzRojaContext cruzRojaContext = new CruzRojaContext();

                var estates = cruzRojaContext.Estates.Where(x => x.EstateID.Equals(resources.FK_EstateID))
                                                                    .Include(a => a.LocationAddress)
                                                                    .Include(a => a.Locations)
                                                                    .AsNoTracking()
                                                                    .FirstOrDefault();

                var status = resources.Donation ? "Si" : "No";

                message = $@"
                                <div style='margin-top: 1.7rem; text-align: center;'>
                                    <img src='https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png' style='
                                    width: 30px;
                                    border-radius: 50%;
                                    padding: 8px;
                                    border: 1px solid #000;'>

                                    <h1 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px;'>SICREYD</h1>
                                </div>
                                     <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                    <p style='margin-left: 20px;'>
                                    A continuación se describen los datos del material: 
                                    </p>

                               <table style='border-collapse: collapse; margin:auto;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Nombre del atributo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor</th>
                                    </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Nombre</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{resources.Name}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Marca</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{resources.Materials.Brand}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Cantidad</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{resources.Quantity}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>¿es donación?</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{status}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates.LocationAddress.Address} {estates.LocationAddress.NumberAddress} {estates.EstateTypes.ToUpper()}</td>
                                              </tr>

                                    </table>

                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Creado: {userCreate.Persons.LastName}, {userCreate.Persons.FirstName}
                                    </p>
                                    
                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Fecha de creación: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}
                                    </p>

                                    {messageFinal}
                                ";
            }
            else if (estado == "Modificación de" && materialToPatch.Enabled)
            {
                string responsable = "";
                string instruction = "";
                string alert = "";
                foreach (var item in _materials.Operations.ToList())
                {
                    var name = item.path.Substring(1);
                    var value = item.value;
                    CruzRojaContext cruzRojaContext2 = new CruzRojaContext();

                    var material = cruzRojaContext2.Materials.Where(x => x.ID.Equals(materials.ID))
                                                          .AsNoTracking()
                                                          .FirstOrDefault();

                    var estates2 = cruzRojaContext2.Estates.Where(x => x.EstateID.Equals(material.FK_EstateID))
                                                            .Include(a => a.LocationAddress)
                                                            .Include(a => a.Locations)
                                                            .AsNoTracking()
                                                            .FirstOrDefault();

                    var estates3 = new Estates();
                    if (name.Equals("fk_EstateID"))
                    {
                        estates3 = cruzRojaContext2.Estates.Where(x => x.EstateID.Equals(Convert.ToInt32(item.value)))
                                                                           .Include(a => a.LocationAddress)
                                                                           .Include(a => a.Locations)
                                                                           .AsNoTracking()
                                                                           .FirstOrDefault();
                    }

                    if (name.Equals("Name"))
                    {
                        //var empleado = EmployeesRepository.GetAllEmployeesById(Convert.ToInt32(item.value));
                        //var empleadoAnterior = EmployeesRepository.GetAllEmployeesById(emergenciesDisasters1.Fk_EmplooyeeID);

                        responsable = $@"
                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Nombre</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{material.MaterialName}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }

                    if (name.Equals("quantity"))
                    {
                        //var DescAnterior = EmployeesRepository.GetAllEmployeesById(emergenciesDisasters1.Fk_EmplooyeeID);

                        instruction = $@"  <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Cantidad</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{material.MaterialQuantity}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";


                    }

                    if (name.Equals("fk_EstateID"))
                    {

                        alert = $@"        <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates2.LocationAddress.Address} {estates2.LocationAddress.NumberAddress} {estates2.EstateTypes.ToUpper()}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates3.LocationAddress.Address} {estates3.LocationAddress.NumberAddress} {estates3.EstateTypes.ToUpper()}</td>
                                              </tr>";
                    }


                    var modificado = EmployeesRepository.GetAllEmployeesById(materials.ModifiedBy);

                    message = $@"
                                <div style='margin-top: 1.7rem; text-align: center;'>
                                    <img src='https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png' style='
                                    width: 30px;
                                    border-radius: 50%;
                                    padding: 8px;
                                    border: 1px solid #000;'>

                                    <h1 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px;'>SICREYD</h1>
                                </div>
                                     <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                    <p style='margin-left: 20px;'>
                                    A continuación se listan los campos modificados:
                                    </p>
                                 <table style='border-collapse: collapse; margin:auto;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Nombre del atributo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor anterior</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor actual</th>
                                    </tr>
                                    {responsable}
                                    {instruction}
                                    {alert}
                                    </table>
                                  <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Modificado por: {modificado.Persons.LastName}, {modificado.Persons.FirstName}
                                         </p>

                                    {messageFinal}
                                ";
                }
            }


            else
            {
               
                estado = "Deshabilitación del";
                var modificado = EmployeesRepository.GetAllEmployeesById(materials.ModifiedBy);
                var status = materials.MaterialDonation  ? "Si" : "No";

                CruzRojaContext cruzRojaContext = new CruzRojaContext();

                var estates = cruzRojaContext.Estates.Where(x => x.EstateID.Equals(materials.FK_EstateID))
                                                                    .Include(a => a.LocationAddress)
                                                                    .Include(a => a.Locations)
                                                                    .AsNoTracking()
                                                                    .FirstOrDefault();

                message = $@"
                                <div style='margin-top: 1.7rem; text-align: center;'>
                                    <img src='https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png' style='
                                    width: 30px;
                                    border-radius: 50%;
                                    padding: 8px;
                                    border: 1px solid #000;'>

                                    <h1 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px;'>SICREYD</h1>
                                </div>
                                     <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                    <p style='margin-left: 20px;'>
                                    A continuación se describen los datos del material: 
                                    </p>

                               <table style='border-collapse: collapse; margin:auto;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Nombre del atributo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor</th>
                                    </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Nombre</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{materials.MaterialName}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Marca</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{materials.MaterialBrand}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Cantidad</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{materials.MaterialQuantity}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>¿es donación?</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{status}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates.LocationAddress.Address} {estates.LocationAddress.NumberAddress} {estates.EstateTypes.ToUpper()}</td>
                                              </tr>
                                    </table>

                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Modificado por: {modificado.Persons.LastName}, {modificado.Persons.FirstName}
                                    </p>

                                    {messageFinal}
                                ";
            }


            foreach (var item in coordinadoraGeneral)
            {
                var msg = new Mail(new string[] { item.Persons.Email }, $"{estado} material #{materials.ID}", $@"{message}");

                EmailSender.SendEmail(msg);
            }
        } 
     }
}

