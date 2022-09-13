using Back_End.Models;
using Back_End.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Vehicles___Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IVehiclesRepository : IRepositoryBase<Vehicles>
    {
        Task<IEnumerable<Vehicles>> GetAllVehiclesFilters(int userId);

        IEnumerable<Vehicles> GetAllVehicles(int userId);
        IEnumerable<Vehicles> GetAllVehicles(DateTime dateStart, DateTime dateEnd);

        Task<Vehicles> GetVehicleById(string vehicleId);
        Task<Vehicles> GetVehicleWithDetails(string vehicleId);

        void CreateVehicle(Vehicles vehicles);

        void UpdateVehicle(Vehicles vehicles);

        void DeleteVehicle(Vehicles vehicles);
        IEnumerable<Vehicles> GetAllVehicles(DateTime dateConvert, DateTime dateConvertEnd, int estateId);
    }
}
