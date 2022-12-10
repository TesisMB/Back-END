using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Medicines___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class MedicinesRepository : RepositoryBase<Medicines>, IMedicinesRepository
    {
        private CruzRojaContext _cruzRojaContext;
        private readonly BlobServiceClient _blobServiceClient;

        public MedicinesRepository(CruzRojaContext cruzRojaContext, BlobServiceClient blobServiceClient)
            : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
            _blobServiceClient = blobServiceClient;

        }

        public async Task<IEnumerable<Medicines>> GetAllMedicines(int userId, int locationId)
        {
            //var medicines = UsersRepository.authUser;

            var user = EmployeesRepository.GetAllEmployeesById(userId);

            var collection = _cruzRojaContext.Medicines as IQueryable<Medicines>;


            if (!locationId.Equals(0))
                collection = collection.Where
                                            (a => a.Estates.Locations.LocationID.Equals(locationId));
            else
                return await GetAllMedicines();

            return await collection
                .Include(a => a.Estates)
                .ThenInclude(a => a.LocationAddress)
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

        public async Task<IEnumerable<Medicines>> GetAllMedicines()
        {
            return await FindAll()
            .Include(a => a.Estates)
                .ThenInclude(a => a.LocationAddress)
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

        public static void status(Medicines medicines)
        {

            CruzRojaContext cruzRojaContext = new CruzRojaContext();
            medicines.Resources_RequestResources_Materials_Medicines_Vehicles = null;
            cruzRojaContext.Update(medicines);
            cruzRojaContext.SaveChanges();

        }


        public async Task<Medicines> GetMedicineById(string medicineId)
        {
            return await FindByCondition(med => med.ID.Equals(medicineId))
                              .Include(a => a.Estates)
                              .ThenInclude(a => a.LocationAddress)
                              .Include(a => a.Estates.EstatesTimes)
                              .ThenInclude(a => a.Times)
                              .ThenInclude(a => a.Schedules)
                              .FirstOrDefaultAsync();
        }

        public async Task<Medicines> GetMedicinelWithDetails(string medicineId)
        {
            return await FindByCondition(med => med.ID.Equals(medicineId))
                     .Include(a => a.Estates)
                     .ThenInclude(a => a.LocationAddress)
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

        public void CreateMedicine(Medicines medicine)
        {
            //spaceCamelCase(medicine);

            //Create(medicine);

            sendEmail(medicine, "Nuevo", null, null, null);
        }

        private void spaceCamelCase(Medicines medicine)
        {
            medicine.MedicineName = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(medicine.MedicineName);
            medicine.MedicineLab = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(medicine.MedicineLab);
            medicine.MedicineDrug = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(medicine.MedicineDrug);
            medicine.MedicineUnits = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(medicine.MedicineUnits);
            medicine.MedicineUtility = WithoutSpace_CamelCase.GetWithoutSpace(medicine.MedicineUtility);
            //Sacar WithoutSpace_CamelCase de Title case //dos funciones separadas
            //     medicine.MedicineName = WithoutSpace_CamelCase.GetWithoutSpace(medicine.MedicineName);
            //   medicine.MedicineLab = WithoutSpace_CamelCase.GetCamelCase(medicine.MedicineLab);
            // medicine.MedicineDrug = WithoutSpace_CamelCase.GetCamelCase(medicine.MedicineDrug);
            //medicine.MedicineUtility = WithoutSpace_CamelCase.GetCamelCase(medicine.MedicineUtility);
        }

        public void UpdateMedicine(Medicines medicine, JsonPatchDocument<MedicineForUpdateDto> _medicines, MedicineForUpdateDto medicineToPatch)
        {
            //Update(medicine);
            sendEmail(medicine, "Modificación de", _medicines, null, medicineToPatch);
        }

        public void DeleteMedicine(Medicines medicine)
        {
            Delete(medicine);
        }


 

        public async Task Upload(MedicineImg medicine)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("publicuploads");

            var blobClient = blobContainer.GetBlobClient(medicine.ImageFile.FileName);

            var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpeg" };

            //await blobClient.UploadAsync(medicine.ImageFile.OpenReadStream());

            await blobClient.UploadAsync(medicine.ImageFile.OpenReadStream(), new BlobUploadOptions { HttpHeaders = blobHttpHeader });
        }

        public async Task<byte[]> Get(string imageName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("publicuploads");

            var blobClient = blobContainer.GetBlobClient(imageName);
            var downloadContent = await blobClient.DownloadAsync();
            using (MemoryStream ms = new MemoryStream())
            {
                await downloadContent.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public IEnumerable<Medicines> GetAllMedicines(DateTime dateStart, DateTime dateEnd, int estateId)
        {
            var collection = _cruzRojaContext.Medicines as IQueryable<Medicines>;
            var user = EmployeesRepository.GetAllEmployeesById(estateId);

            var fecha = Convert.ToDateTime("01/01/0001");

            if (dateEnd == fecha)
            {
                collection = collection.Where(
                                                a => a.MedicineDateCreated >= dateStart &&
                                                a.MedicineAvailability == false
                                                && a.FK_EstateID == estateId);
            }
            else
            {
                collection = collection.Where(
                                a => a.MedicineDateCreated >= dateStart && a.MedicineDateCreated <= dateEnd
                                &&
                                a.MedicineAvailability == false
                                && a.FK_EstateID == user.FK_EstateID
                               );
            }



            return collection
                .Include(a => a.Estates)
                .ThenInclude(a => a.LocationAddress)
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


        public static void sendEmail(Medicines medicine, string estado,
                                        JsonPatchDocument<MedicineForUpdateDto> _medicines = null,
                                        Resources_ForCreationDto resources = null,
                                        MedicineForUpdateDto medicineToPatch = null)
        {

            string message = "";
            string messageFinal;
            CruzRojaContext cruzRojaContext1 = new CruzRojaContext();


            //var med = cruzRojaContext1.Medicines.Where(x => x.ID.Equals(medicine.ID))
            //                                      .AsNoTracking()
            //                                      .FirstOrDefault();

            var coordinadoraGeneral = cruzRojaContext1.Users.Where(x => x.FK_EstateID.Equals(2)
                                                                   && x.FK_RoleID == 2)
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
                var userCreate = EmployeesRepository.GetAllEmployeesById(medicine.CreatedBy);

                CruzRojaContext cruzRojaContext = new CruzRojaContext();

                var estates = cruzRojaContext.Estates.Where(x => x.EstateID.Equals(medicine.FK_EstateID))
                                                                    .Include(a => a.LocationAddress)
                                                                    .Include(a => a.Locations)
                                                                    .AsNoTracking()
                                                                    .FirstOrDefault();

                var status = medicine.MedicineDonation ? "Si" : "No";

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
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineName}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Cantidad</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineQuantity}</td>
                                              </tr>

                                               <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Farmaco</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineDrug}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Laboratorio</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineLab}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Peso - Volumen</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineWeight}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Unidad de medida</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineUnits}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>¿es donación?</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{status}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Descripción</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineUtility}</td>
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
            else if (estado == "Modificación de" && !medicine.Enabled)
            {
                string responsable = "";
                string instruction = "";
                string alert = "";
                string units = "";
                string descripcion = "";

                foreach (var item in _medicines.Operations.ToList())
                {
                    var name = item.path.Substring(1);
                    var value = item.value;
                    CruzRojaContext cruzRojaContext2 = new CruzRojaContext();

                    var med = cruzRojaContext2.Medicines.Where(x => x.ID.Equals(medicine.ID))
                                                          .AsNoTracking()
                                                          .FirstOrDefault();

                    var estates2 = cruzRojaContext2.Estates.Where(x => x.EstateID.Equals(med.FK_EstateID))
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

                    if (name.Equals("name"))
                    {
                        responsable = $@"
                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Nombre</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{med.MedicineName}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }

                    if (name.Equals("quantity"))
                    {
                        instruction = $@"  <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Cantidad</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{med.MedicineQuantity}</td>
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

                    if (name.Equals("medicines/medicineUnits"))
                    {
                        units = $@"        <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{med.MedicineUnits}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }

                    if (name.Equals("description"))
                    {
                        units = $@"        <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{med.MedicineUtility}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }
                }


                var modificado = EmployeesRepository.GetAllEmployeesById(medicine.ModifiedBy);

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
                var modificado = EmployeesRepository.GetAllEmployeesById(medicine.ModifiedBy);
                var status = medicine.MedicineDonation ? "Si" : "No";

                CruzRojaContext cruzRojaContext = new CruzRojaContext();

                var estates = cruzRojaContext.Estates.Where(x => x.EstateID.Equals(2))
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
                                    A continuación se describen los datos del medicamento: 
                                    </p>

                               <table style='border-collapse: collapse; margin:auto;'>
                                   <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Nombre del atributo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor</th>
                                    </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Nombre</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineName}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Cantidad</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineQuantity}</td>
                                              </tr>

                                               <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Farmaco</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineDrug}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Laboratorio</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineLab}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Peso - Volumen</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineWeight}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Unidad de medida</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineUnits}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>¿es donación?</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{status}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Descripción</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicine.MedicineUtility}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates.LocationAddress.Address} {estates.LocationAddress.NumberAddress} {estates.EstateTypes.ToUpper()}</td>
                                              </tr>

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
                var msg = new Mail(new string[] { item.Persons.Email }, $"{estado} medicamento #{medicine.ID}", $@"{message}");

                EmailSender.SendEmail(msg);
            }
        }
    }

}

