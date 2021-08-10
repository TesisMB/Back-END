using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class VehiclesRepository : RepositoryBase<Vehicles>, IVehiclesRepository
    {
        public VehiclesRepository(CruzRojaContext cruzRojaContext2)
             : base(cruzRojaContext2)
        {

        }
        public async Task<IEnumerable<Vehicles>> GetAllVehicles()
        {
            return await FindAll()
                   .Include(a => a.Estates)
                   .Include(a => a.Estates.LocationAddress)
                   .Include(a => a.Estates.EstatesTimes)
                   .ThenInclude(a => a.Times)
                   .ThenInclude(a => a.Schedules)
                   .Include(a => a.Employees)
                   .ThenInclude(a => a.Users)
                   .ThenInclude(a => a.Persons)
                   .Include(a => a.TypeVehicles)
                 .ToListAsync();
        }

        public async Task<Vehicles> GetVehicleById(int vehicleId)
        {
            return await FindByCondition(vehicle => vehicle.VehicleID == vehicleId)
                .FirstOrDefaultAsync();
        }
        public async Task<Vehicles> GetVehicleWithDetails(int vehicleId)
        {
            return await FindByCondition(vehicle => vehicle.VehicleID == vehicleId)
                      .Include(a => a.Estates)
                      .Include(a => a.Estates.LocationAddress)
                      .Include(a => a.Estates.EstatesTimes)
                      .ThenInclude(a => a.Times)
                      .ThenInclude(a => a.Schedules)
                      .Include(a => a.Employees)
                      .ThenInclude(a => a.Users)
                      .ThenInclude(a => a.Persons)
                      .Include(a => a.TypeVehicles)
                   .FirstOrDefaultAsync();
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
