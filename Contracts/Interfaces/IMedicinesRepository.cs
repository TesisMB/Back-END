using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Interfaces
{
    public interface IMedicinesRepository : IRepositoryBase<Medicines>
    {
        IEnumerable<Medicines> GetAllMedicines();

        Medicines GetMedicineById(int medicineId);

        Medicines GetMedicinelWithDetails(int medicineId);

        void CreateMedicine(Medicines medicine);
        void UpdateMedicine(Medicines medicine);

        void DeleteMedicine(Medicines medicine);
    }
}
