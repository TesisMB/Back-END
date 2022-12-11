using Back_End.Models;
using Back_End.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Vehicles___Dto;
using Entities.DataTransferObjects.Vehicles___Dto.Update;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IVehiclesRepository : IRepositoryBase<Vehicles>
    {
        Task<IEnumerable<Vehicles>> GetAllVehiclesFilters(int userId, int locationId);

        IEnumerable<Vehicles> GetAllVehicles(int userId);
        Task<IEnumerable<Vehicles>> GetAllVehicles();

        Task<Vehicles> GetVehicleById(string vehicleId);
        Task<Vehicles> GetVehicleWithDetails(string vehicleId);

        void CreateVehicle(Vehicles vehicles, int userId);

        void UpdateVehicle(Vehicles vehicles, JsonPatchDocument<VehiclesForUpdateDto> patchDocument, int userId);

        void DeleteVehicle(Vehicles vehicles);
        IEnumerable<Vehicles> GetAllVehicles(DateTime dateConvert, DateTime dateConvertEnd, int estateId);
    }
}
