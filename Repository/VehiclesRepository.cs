using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.Vehicles___Dto.Update;
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
    public class VehiclesRepository : RepositoryBase<Vehicles>, IVehiclesRepository
    {

        private CruzRojaContext _cruzRojaContext;
        public VehiclesRepository(CruzRojaContext cruzRojaContext)
             : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }
        public async Task<IEnumerable<Vehicles>> GetAllVehiclesFilters(int userId, int locationId)
        {
            var user = await _cruzRojaContext.Users
                            .Where(a => a.UserID.Equals(userId))
                            .Include(a => a.Roles)
                            .Include(a => a.Estates)
                            .ThenInclude(a => a.Locations)
                            .FirstOrDefaultAsync();
            var collection = _cruzRojaContext.Vehicles as IQueryable<Vehicles>;

            if (!locationId.Equals(0))
                collection = collection.Where
                                            (a => a.Estates.Locations.LocationID.Equals(locationId));
            else
                return await GetAllVehicles();

            return await collection
                      .Include(a => a.Estates)
                      .Include(a => a.Estates.LocationAddress)
                      .Include(a => a.Estates.EstatesTimes)
                      .ThenInclude(a => a.Times)
                      .ThenInclude(a => a.Schedules)
                      .Include(a => a.Employees)
                      .ThenInclude(a => a.Users)
                      .ThenInclude(a => a.Persons)
                      .Include(a => a.TypeVehicles)
                      .Include(a => a.Estates.Locations)
                      .Include(a => a.Brands)
                      .Include(a => a.Model)

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

        public async Task<Vehicles> GetVehicleById(string vehicleId)
        {
            return await FindByCondition(vehicle => vehicle.ID == vehicleId)
                .FirstOrDefaultAsync();
        }

        public async Task<Vehicles> GetVehicleWithDetails(string vehicleId)
        {
            return await FindByCondition(vehicle => vehicle.ID == vehicleId)
                      .Include(a => a.Estates)
                      .Include(a => a.Estates.LocationAddress)
                      .Include(a => a.Estates.EstatesTimes)
                      .ThenInclude(a => a.Times)
                      .ThenInclude(a => a.Schedules)
                      .Include(a => a.Employees)
                      .ThenInclude(a => a.Users)
                      .ThenInclude(a => a.Persons)
                      .Include(a => a.TypeVehicles)
                      .Include(a => a.Estates.Locations)
                      .Include(a => a.Brands)
                      .Include(a => a.Model)

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


        public static void status(Vehicles Vehicles)
        {

            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            cruzRojaContext.Update(Vehicles);

            cruzRojaContext.SaveChanges();
        
        }


        public void CreateVehicle(Vehicles vehicles, int userId)
        {
            //spaceCamelCase(vehicles);
            Create(vehicles);
            sendEmail(vehicles, "Nuevo", userId, null, null, null);
        }

        private void spaceCamelCase(Vehicles vehicles)
        {
            vehicles.VehiclePatent = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(vehicles.VehiclePatent);
            vehicles.VehicleUtility = WithoutSpace_CamelCase.GetWithoutSpace(vehicles.VehicleUtility);
            vehicles.VehicleDescription = WithoutSpace_CamelCase.GetWithoutSpace(vehicles.VehicleDescription);

            if (vehicles.TypeVehicles != null)
            {
                vehicles.TypeVehicles.Type = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(vehicles.TypeVehicles.Type);
            }

          /*  if (vehicles.BrandsModels.Brands != null || vehicles.BrandsModels.Model != null)
            {
                vehicles.BrandsModels.Brands.BrandName = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(vehicles.BrandsModels.Brands.BrandName);
                vehicles.BrandsModels.Model.ModelName = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(vehicles.BrandsModels.Model.ModelName);
            }*/
        }

        public void UpdateVehicle(Vehicles vehicles, JsonPatchDocument<VehiclesForUpdateDto> patchDocument, int userId)
        {
            Update(vehicles);
            //sacar despues id
            sendEmail(vehicles, "Modificación de", userId, patchDocument, null, null);

        }

        public void DeleteVehicle(Vehicles vehicles)
        {
            Delete(vehicles);
        }

        public IEnumerable<Vehicles> GetAllVehicles(int userId)
        {

            var user = EmployeesRepository.GetAllEmployeesById(userId);

            var collection = _cruzRojaContext.Vehicles as IQueryable<Vehicles>;

            collection = collection.Where(
                                         a => a.Estates.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);

            return collection
                   .Include(a => a.Estates)
                   .Include(a => a.Estates.LocationAddress)
                   .Include(a => a.Estates.EstatesTimes)
                   .ThenInclude(a => a.Times)
                   .ThenInclude(a => a.Schedules)
                   .Include(a => a.Employees)
                   .ThenInclude(a => a.Users)
                   .ThenInclude(a => a.Persons)
                   .Include(a => a.TypeVehicles)
                   .Include(a => a.Estates.Locations)
                   .Include(a => a.Brands)
                   .Include(a => a.Model)

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



        public async Task<IEnumerable<Vehicles>> GetAllVehicles()
        {
            return await FindAll()
                       .Include(a => a.Estates)
                      .Include(a => a.Estates.LocationAddress)
                      .Include(a => a.Estates.EstatesTimes)
                      .ThenInclude(a => a.Times)
                      .ThenInclude(a => a.Schedules)
                      .Include(a => a.Employees)
                      .ThenInclude(a => a.Users)
                      .ThenInclude(a => a.Persons)
                      .Include(a => a.TypeVehicles)
                      .Include(a => a.Estates.Locations)
                      .Include(a => a.Brands)
                      .Include(a => a.Model)

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

        public IEnumerable<Vehicles> GetAllVehicles(DateTime dateStart, DateTime dateEnd, int estateId)
        {
            var user = EmployeesRepository.GetAllEmployeesById(estateId);


            var collection = _cruzRojaContext.Vehicles as IQueryable<Vehicles>;
            var fecha = Convert.ToDateTime("01/01/0001");

            if (dateEnd == fecha)
            {
                collection = collection.Where(
                                             a => a.VehicleDateCreated >= dateStart 
                                             && a.VehicleAvailability == false
                                             && a.FK_EstateID == estateId);
            }
            else
            {
                collection = collection.Where(
                                         a => a.VehicleDateCreated >= dateStart 
                                         && a.VehicleDateCreated <= dateEnd
                                         && a.VehicleAvailability == false
                                         && a.FK_EstateID == user.FK_EstateID);
            }

            return collection
                   .Include(a => a.Estates)
                   .Include(a => a.Estates.LocationAddress)
                   .Include(a => a.Estates.EstatesTimes)
                   .ThenInclude(a => a.Times)
                   .ThenInclude(a => a.Schedules)
                   .Include(a => a.Employees)
                   .ThenInclude(a => a.Users)
                   .ThenInclude(a => a.Persons)
                   .Include(a => a.TypeVehicles)
                   .Include(a => a.Estates.Locations)
                   .Include(a => a.Brands)
                   .Include(a => a.Model)
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



        public static void sendEmail(Vehicles vehicles, string estado, int userId,
                                      JsonPatchDocument<VehiclesForUpdateDto> _vehicle = null,
                                      Resources_ForCreationDto resources = null,
                                      VehiclesForUpdateDto vehicleToPatch = null)
        {

            string message = "";
            string messageFinal;
            CruzRojaContext cruzRojaContext1 = new CruzRojaContext();


            //var med = cruzRojaContext1.Medicines.Where(x => x.ID.Equals(medicine.ID))
            //                                      .AsNoTracking()
            //                                      .FirstOrDefault();

            var user = EmployeesRepository.GetAllEmployeesById(userId);


            var coordinadoraGeneral = cruzRojaContext1.Users.Where(x => x.FK_EstateID.Equals(user.FK_EstateID)
                                                                  && x.FK_RoleID != 4 && x.FK_RoleID != 5 && x.FK_RoleID != 1)
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
                var userCreate = EmployeesRepository.GetAllEmployeesById(vehicles.CreatedBy);

                CruzRojaContext cruzRojaContext = new CruzRojaContext();

                var estates = cruzRojaContext.Estates.Where(x => x.EstateID.Equals(vehicles.FK_EstateID))
                                                                    .Include(a => a.LocationAddress)
                                                                    .Include(a => a.Locations)
                                                                    .AsNoTracking()
                                                                    .FirstOrDefault();

                var veh = cruzRojaContext.TypeVehicles.Where(x => x.ID.Equals(vehicles.Fk_TypeVehicleID))
                                                                   .AsNoTracking()
                                                                   .FirstOrDefault();


                var status = vehicles.VehicleDonation ? "Si" : "No";

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
                                                <td style='border: 1px solid #000; padding: 8px;'>Marca</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.Brands.BrandName}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Modelo</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.Model.ModelName}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Patente</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehiclePatent}</td>
                                              </tr>

                                               <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Año</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleYear}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Utilidad</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleUtility}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Tipo de rodado</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{veh.Type}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Descripción</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleDescription}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates.LocationAddress.Address} {estates.LocationAddress.NumberAddress} {estates.EstateTypes.ToUpper()}</td>
                                              </tr>

                                    </table>

                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Creado por: {userCreate.Persons.LastName}, {userCreate.Persons.FirstName}
                                    </p>
                                    
                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Fecha de creación: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}
                                    </p>

                                    {messageFinal}
                                ";
            }
            else if (estado == "Modificación de" && vehicles.Enabled)
            {
                string responsable = "";
                string instruction = "";
                string alert = "";
                string units = "";
                string descripcion = "";

                foreach (var item in _vehicle.Operations.ToList())
                {
                    var name = item.path.Substring(1);
                    var value = item.value;
                    CruzRojaContext cruzRojaContext2 = new CruzRojaContext();

                    var veh = cruzRojaContext2.Vehicles.Where(x => x.ID.Equals(vehicles.ID))
                                                          .AsNoTracking()
                                                          .FirstOrDefault();

                    var estates2 = cruzRojaContext2.Estates.Where(x => x.EstateID.Equals(veh.FK_EstateID))
                                                            .Include(a => a.LocationAddress)
                                                            .Include(a => a.Locations)
                                                            .AsNoTracking()
                                                            .FirstOrDefault();

                  
                    if (name.Equals("vehicles/fK_EmployeeID"))
                    {

                        var respo = cruzRojaContext2.Users.Where(x => x.UserID.Equals(Convert.ToInt32(item.value)))
                                                                        .Include(a => a.Persons)
                                                                        .AsNoTracking()
                                                                        .FirstOrDefault();

                        var respoante = cruzRojaContext2.Users.Where(x => x.UserID.Equals(veh.FK_EmployeeID))
                                                                        .Include(a => a.Persons)
                                                                       .AsNoTracking()
                                                                       .FirstOrDefault();

                        responsable = $@"
                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Responsable</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{respo.Persons.LastName}. {respo.Persons.FirstName}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{respoante.Persons.LastName}. {respoante.Persons.FirstName}</td>
                                              </tr>";
                    }

                    if (name.Equals("vehicles/vehicleUtility"))
                    {
                        instruction = $@"  <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Utilidad</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{veh.VehicleUtility}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";


                    }

                    if (name.Equals("vehicles/Fk_TypeVehicleID"))
                    {

                        var type = cruzRojaContext2.TypeVehicles.Where(x => x.ID.Equals(veh.Fk_TypeVehicleID))
                                                    .AsNoTracking()
                                                    .FirstOrDefault();

                        var typeActual = cruzRojaContext2.TypeVehicles.Where(x => x.ID.Equals(Convert.ToInt32(item.value)))
                                                .AsNoTracking()
                                                .FirstOrDefault();

                        alert = $@"        <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Tipo de rodado</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{type.Type}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{typeActual.Type}</td>
                                              </tr>";
                    }

                    if (name.Equals("description"))
                    {
                        units = $@"        <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Descipción</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{veh.VehicleDescription}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }

                    if (name.Equals("fk_EstateID"))
                    {
                        var estates3 = cruzRojaContext2.Estates.Where(x => x.EstateID.Equals(Convert.ToInt32(item.value)))
                                                                  .Include(a => a.LocationAddress)
                                                                  .Include(a => a.Locations)
                                                                  .AsNoTracking()
                                                                  .FirstOrDefault();

                        units = $@"        <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates2.LocationAddress.Address} {estates2.LocationAddress.NumberAddress} {estates2.EstateTypes.ToUpper()}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates3.LocationAddress.Address} {estates3.LocationAddress.NumberAddress} {estates3.EstateTypes.ToUpper()}</td>
                                              </tr>";
                    }
                }


                var modificado = EmployeesRepository.GetAllEmployeesById(vehicles.ModifiedBy);

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
                                    {units}
                                    {descripcion}
                                    </table>
                                  <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Modificado por: {modificado.Persons.LastName}, {modificado.Persons.FirstName}
                                         </p>

                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Fecha de modificación: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}
                                    </p>
                                    {messageFinal}
                                ";

            }


            else
            {

                estado = "Deshabilitación del";
                var modificado = EmployeesRepository.GetAllEmployeesById(vehicles.ModifiedBy);
                var status = vehicles.VehicleDonation ? "Si" : "No";

                CruzRojaContext cruzRojaContext = new CruzRojaContext();

                var estates = cruzRojaContext.Estates.Where(x => x.EstateID.Equals(2))
                                                                    .Include(a => a.LocationAddress)
                                                                    .Include(a => a.Locations)
                                                                    .AsNoTracking()
                                                                    .FirstOrDefault();

                var veh = cruzRojaContext.TypeVehicles.Where(x => x.ID.Equals(vehicles.Fk_TypeVehicleID))
                                                           .AsNoTracking()
                                                           .FirstOrDefault();

                var vehicl = cruzRojaContext.Vehicles.Where(x => x.ID.Equals(vehicles.ID))
                                                          .Include(x => x.Brands)
                                                          .Include(x => x.Model)
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
                                    A continuación se describen los datos del vehiculo: 
                                    </p>

                              <table style='border-collapse: collapse; margin:auto;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Nombre del atributo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor</th>
                                    </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Marca</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicl.Brands.BrandName}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Modelo</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicl.Model.ModelName}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Patente</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehiclePatent}</td>
                                              </tr>

                                               <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Año</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleYear}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Utilidad</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleUtility}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Tipo de rodado</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{veh.Type}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Descripción</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleDescription}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates.LocationAddress.Address} {estates.LocationAddress.NumberAddress} {estates.EstateTypes.ToUpper()}</td>
                                              </tr>

                                    </table>
                                    </table>

                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Modificado por: {modificado.Persons.LastName}, {modificado.Persons.FirstName}
                                    </p>

                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Fecha de deshabilitación: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}
                                    </p>

                                    {messageFinal}
                                ";
            }


            foreach (var item in coordinadoraGeneral)
            {
                var msg = new Mail(new string[] { item.Persons.Email }, $"{estado} vehiculo #{vehicles.ID}", $@"{message}");

                EmailSender.SendEmail(msg);
            }
        }

    }
}
