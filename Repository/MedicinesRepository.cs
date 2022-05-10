using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class MedicinesRepository : RepositoryBase<Medicines>, IMedicinesRepository
    {
        private CruzRojaContext _cruzRojaContext;
        public MedicinesRepository(CruzRojaContext cruzRojaContext)
            : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }

        public async Task<IEnumerable<Medicines>> GetAllMedicines()
        {
            var medicines = UsersRepository.authUser;

            var collection = _cruzRojaContext.Medicines as IQueryable<Medicines>;

            collection = collection.Where(
                                            a => a.Estates.Locations.LocationDepartmentName == medicines.Estates.Locations.LocationDepartmentName);

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
            spaceCamelCase(medicine);

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
    }
}
