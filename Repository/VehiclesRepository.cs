using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Repository
{
    public class VehiclesRepository : RepositoryBase<Vehicles>, IVehiclesRepository
    {
        public VehiclesRepository(CruzRojaContext cruzRojaContext2)
             : base(cruzRojaContext2)
        {

        }
        public IEnumerable<Vehicles> GetAllVehciles()
        {
            return FindAll()
                 .Include(a => a.TypeVehicles)
                 .ToList();
        }

        public Vehicles GetVehicleById(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public void CreateVehicle(Vehicles vehicles)
        {
            throw new NotImplementedException();
        }

        public void UpdateVehicle(Vehicles vehicles)
        {
            throw new NotImplementedException();
        }

        public void DeleteVehicle(Vehicles vehicles)
        {
            throw new NotImplementedException();
        }

    }
}
