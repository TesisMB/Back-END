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

        private CruzRojaContext _cruzRojaContext;
        public VehiclesRepository(CruzRojaContext cruzRojaContext)
             : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }
        public async Task<IEnumerable<Vehicles>> GetAllVehicles()
        {
            var vehicles = UsersRepository.authUser;

            var collection = _cruzRojaContext.Vehicles as IQueryable<Vehicles>;

            collection = collection.Where(
                                        a => a.Estates.Locations.LocationDepartmentName == vehicles.Estates.Locations.LocationDepartmentName);

            return await collection
                   .Include(a => a.Estates)
                   .Include(a => a.Estates.LocationAddress)
                   .Include(a => a.Estates.EstatesTimes)
                   .ThenInclude(a => a.Times)
                   .ThenInclude(a => a.Schedules)
                   .Include(a => a.Employees)
                   .ThenInclude(a => a.Users)
                   .ThenInclude(a => a.Persons)
                   .Include(a => a.Type)
                   .Include(a => a.Estates.Locations)
                   .Include(a => a.BrandsModels)
                   .Include(a => a.BrandsModels.Brands)
                   .Include(a => a.BrandsModels.Model)
                 .ToListAsync();
        }

        public async Task<Vehicles> GetVehicleById(int vehicleId)
        {
            return await FindByCondition(vehicle => vehicle.ID == vehicleId)
                .FirstOrDefaultAsync();
        }
        public async Task<Vehicles> GetVehicleWithDetails(int vehicleId)
        {
            return await FindByCondition(vehicle => vehicle.ID == vehicleId)
                      .Include(a => a.Estates)
                      .Include(a => a.Estates.LocationAddress)
                      .Include(a => a.Estates.EstatesTimes)
                      .ThenInclude(a => a.Times)
                      .ThenInclude(a => a.Schedules)
                      .Include(a => a.Employees)
                      .ThenInclude(a => a.Users)
                      .ThenInclude(a => a.Persons)
                      .Include(a => a.Type)
                      .Include(a => a.Estates.Locations)
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
