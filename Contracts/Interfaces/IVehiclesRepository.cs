using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IVehiclesRepository : IRepositoryBase<Vehicles>
    {
        Task<IEnumerable<Vehicles>> GetAllVehicles();

        Task<Vehicles> GetVehicleById(int vehicleId);
        Task<Vehicles> GetVehicleWithDetails(int vehicleId);

        void CreateVehicle(Vehicles vehicles);

        void UpdateVehicle(Vehicles vehicles);

        void DeleteVehicle(Vehicles vehicles);
    }
}
