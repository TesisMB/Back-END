using Back_End.Entities;
using Contracts.Interfaces;
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
                .ToListAsync();
        }

        public async Task<Medicines> GetMedicineById(int medicineId)
        {
            return await FindByCondition(med => med.ID.Equals(medicineId))
                              //.Include(a => a.Estates)
                              //.ThenInclude(a => a.LocationAddress)
                              //.Include(a => a.Estates.EstatesTimes)
                              //.ThenInclude(a => a.Times)
                              //.ThenInclude(a => a.Schedules)
                              .FirstOrDefaultAsync();
        }

        public async Task<Medicines> GetMedicinelWithDetails(int medicineId)
        {
            return await FindByCondition(med => med.ID.Equals(medicineId))
                     .Include(a => a.Estates)
                     .ThenInclude(a => a.LocationAddress)
                     .Include(a => a.Estates.EstatesTimes)
                     .ThenInclude(a => a.Times)
                     .ThenInclude(a => a.Schedules)
                     .Include(a => a.Estates.Locations)
                     .FirstOrDefaultAsync();
        }

        public void CreateMedicine(Medicines medicine)
        {
            Create(medicine);
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
