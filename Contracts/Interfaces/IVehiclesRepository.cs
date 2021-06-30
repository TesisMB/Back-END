using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Interfaces
{
    public interface IVehiclesRepository : IRepositoryBase<Vehicles>
    {
        IEnumerable<Vehicles> GetAllVehicles();

        Vehicles GetVehicleById(int vehicleId);

        void CreateVehicle(Vehicles vehicles);

        void UpdateVehicle(Vehicles vehicles);

        void DeleteVehicle(Vehicles vehicles);
    }
}
