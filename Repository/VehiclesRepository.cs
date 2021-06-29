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
        public IEnumerable<Vehicles> GetAllVehicles()
        {
            return FindAll()
                   .Include(a => a.Estates)
                   .Include(a => a.Estates.LocationAddress) 
                   .Include(a => a.Estates.EstatesTimes)
                   .ThenInclude( a => a.Times)
                   .ThenInclude(a => a.Schedules)
                   .Include(a => a.Employees)
                   .ThenInclude(a => a.Users)
                   .ThenInclude(a => a.Persons)
                   .Include(a => a.TypeVehicles)
                 .ToList();
        }

        public Vehicles GetVehicleById(int vehicleId)
        {
            return FindByCondition(vehicle => vehicle.VehicleID == vehicleId)
                   .Include(a => a.Estates)
                   .Include(a => a.Estates.LocationAddress)
                   .Include(a => a.Estates.EstatesTimes)
                   .ThenInclude(a => a.Times)
                   .ThenInclude(a => a.Schedules)
                   .Include(a => a.Employees)
                   .ThenInclude(a => a.Users)
                   .ThenInclude(a => a.Persons)
                   .Include(a => a.TypeVehicles)
                .FirstOrDefault();
        }

        public void CreateVehicle(Vehicles vehicles)
        {
            Create(vehicles);
        }

        public void UpdateVehicle(Vehicles vehicles)
        {
            Update(vehicles);
        }

        public void DeleteVehicle(Vehicles vehicles)
        {
            Delete(vehicles);
        }

    }
}
