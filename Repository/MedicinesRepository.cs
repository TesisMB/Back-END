using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
   public class MedicinesRepository : RepositoryBase<Medicines>, IMedicinesRepository
    {
        public MedicinesRepository(CruzRojaContext cruzRojaContext2)
            : base(cruzRojaContext2)
        {

        }

        public void CreateMedicine(Medicines medicine)
        {
            Create(medicine);
        }

        public void DeleteMedicine(Medicines medicine)
        {
            Delete(medicine);
        }

        public IEnumerable<Medicines> GetAllMedicines()
        {
            return FindAll()
                .Include(a => a.Estates)
                .ThenInclude(a => a.LocationAddress)
                .Include(a => a.Estates.EstatesTimes)
                .ThenInclude(a => a.Times)
                .ThenInclude(a => a.Schedules)
                .ToList();
        }

        public Medicines GetMedicineById(int medicineId)
        {
            return FindByCondition(med => med.MedicineID.Equals(medicineId))
                              //.Include(a => a.Estates)
                              //.ThenInclude(a => a.LocationAddress)
                              //.Include(a => a.Estates.EstatesTimes)
                              //.ThenInclude(a => a.Times)
                              //.ThenInclude(a => a.Schedules)
                              .FirstOrDefault();
        }

        public Medicines GetMedicinelWithDetails(int medicineId)
        {
            return FindByCondition(med => med.MedicineID.Equals(medicineId))
                     .Include(a => a.Estates)
                     .ThenInclude(a => a.LocationAddress)
                     .Include(a => a.Estates.EstatesTimes)
                     .ThenInclude(a => a.Times)
                     .ThenInclude(a => a.Schedules)
                     .FirstOrDefault();
        }

        public void UpdateMedicine(Medicines medicine)
        {
            Update(medicine);
        }
    }
}
