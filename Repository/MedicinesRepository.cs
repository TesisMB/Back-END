using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public class MedicinesRepository : RepositoryBase<Medicines>, IMedicinesRepository
    {
        public MedicinesRepository(CruzRojaContext cruzRojaContext)
            : base(cruzRojaContext)
        {

        }

        public async Task<IEnumerable<Medicines>> GetAllMedicines()
        {
            return await FindAll()
                .Include(a => a.Estates)
                .ThenInclude(a => a.LocationAddress)
                .Include(a => a.Estates.EstatesTimes)
                .ThenInclude(a => a.Times)
                .ThenInclude(a => a.Schedules)
                .ToListAsync();
        }

        public async Task<Medicines> GetMedicineById(int medicineId)
        {
            return await FindByCondition(med => med.MedicineID.Equals(medicineId))
                              //.Include(a => a.Estates)
                              //.ThenInclude(a => a.LocationAddress)
                              //.Include(a => a.Estates.EstatesTimes)
                              //.ThenInclude(a => a.Times)
                              //.ThenInclude(a => a.Schedules)
                              .FirstOrDefaultAsync();
        }

        public async Task<Medicines> GetMedicinelWithDetails(int medicineId)
        {
            return await FindByCondition(med => med.MedicineID.Equals(medicineId))
                     .Include(a => a.Estates)
                     .ThenInclude(a => a.LocationAddress)
                     .Include(a => a.Estates.EstatesTimes)
                     .ThenInclude(a => a.Times)
                     .ThenInclude(a => a.Schedules)
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
