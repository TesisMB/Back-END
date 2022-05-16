using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IMedicinesRepository : IRepositoryBase<Medicines>
    {

        Task<IEnumerable<Medicines>> GetAllMedicines(int userId);

        Task<Medicines> GetMedicineById(string medicineId);

        Task<Medicines> GetMedicinelWithDetails(string medicineId);

        void CreateMedicine(Medicines medicine);
        void UpdateMedicine(Medicines medicine);

        void DeleteMedicine(Medicines medicine);
    }
}
