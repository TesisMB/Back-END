using Entities.DataTransferObjects.Medicines___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IMedicinesRepository : IRepositoryBase<Medicines>
    {

        Task<IEnumerable<Medicines>> GetAllMedicines(int userId, int locationId);
        Task<IEnumerable<Medicines>> GetAllMedicines();

        Task<Medicines> GetMedicineById(string medicineId);

        Task<Medicines> GetMedicinelWithDetails(string medicineId);

        void CreateMedicine(Medicines medicine);
        void UpdateMedicine(Medicines medicine, JsonPatchDocument<MedicineForUpdateDto> _medicines, MedicineForUpdateDto medicineToPatch);

        void DeleteMedicine(Medicines medicine);

        Task Upload(MedicineImg medicine);

        Task<byte[]> Get(string imageName);
        IEnumerable<Medicines> GetAllMedicines(DateTime dateConvert, DateTime dateConvertEnd, int estateId);
    }
}

