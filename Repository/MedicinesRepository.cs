using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Back_End.Entities;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Medicines___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.Helpers;
using Entities.Models;
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

        public async Task<IEnumerable<Medicines>> GetAllMedicines(int userId)
        {
            //var medicines = UsersRepository.authUser;

            var user = EmployeesRepository.GetAllEmployeesById(userId);

            var collection = _cruzRojaContext.Medicines as IQueryable<Medicines>;

            collection = collection.Where(
                                            a => a.Estates.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);

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

        public IEnumerable<Medicines> GetAllMedicines(DateTime dateStart, DateTime dateEnd)
        {

            var collection = _cruzRojaContext.Medicines as IQueryable<Medicines>;

            if(dateEnd == null)
            {
            collection = collection.Where(
                                            a => a.MedicineDateCreated >= dateStart ||
                                            a.MedicineAvailability == false && a.MedicineExpirationDate < DateTime.Now
                                           );
            }
            else
            {
                collection = collection.Where(
                                a => a.MedicineDateCreated >= dateStart && a.MedicineDateCreated <= dateEnd
                                &&
                                a.MedicineAvailability == false
                               );
            }

     

            return  collection
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

            Create(medicine);
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

        public void UpdateMedicine(Medicines medicine)
        {
            Update(medicine);
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
    }
}
