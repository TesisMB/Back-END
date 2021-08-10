using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IMedicinesRepository : IRepositoryBase<Medicines>
    {
        Task<IEnumerable<Medicines>> GetAllMedicines();

        Task<Medicines> GetMedicineById(int medicineId);

        Task<Medicines> GetMedicinelWithDetails(int medicineId);

        void CreateMedicine(Medicines medicine);
        void UpdateMedicine(Medicines medicine);

        void DeleteMedicine(Medicines medicine);
    }
}
