using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Vehicles___Dto;
using Entities.Helpers;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Vehicles>> GetAllVehiclesFilters(vehiclesFiltersDto vehicles)
        {
            var vehicles1 = UsersRepository.authUser;

            var collection = _cruzRojaContext.Vehicles as IQueryable<Vehicles>;

            if (string.IsNullOrEmpty(vehicles.Type) && (vehicles.VehicleYear == null))
            {
                return GetAllVehicles();
            }


            if (!string.IsNullOrEmpty(vehicles.Type) && (vehicles.VehicleYear != null))
            {
                collection = collection.Where(
                                      a => a.TypeVehicles.Type == vehicles.Type
                                      &&
                                       a.Estates.Locations.LocationDepartmentName == vehicles1.Estates.Locations.LocationDepartmentName);
            }


            if ((vehicles.VehicleYear != null))
            {
                collection = collection.Where(
                                      a => a.VehicleYear == vehicles.VehicleYear
                                      &&
                                       a.Estates.Locations.LocationDepartmentName == vehicles1.Estates.Locations.LocationDepartmentName);
            }

            return await collection
                      .Include(a => a.Estates)
                      .Include(a => a.Estates.LocationAddress)
                      .Include(a => a.Estates.EstatesTimes)
                      .ThenInclude(a => a.Times)
                      .ThenInclude(a => a.Schedules)
                      .Include(a => a.Employees)
                      .ThenInclude(a => a.Users)
                      .ThenInclude(a => a.Persons)
                      .Include(a => a.TypeVehicles)
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
                      .Include(a => a.TypeVehicles)
                      .Include(a => a.Estates.Locations)
                      .Include(a => a.BrandsModels)
                      .Include(a => a.BrandsModels.Brands)
                      .Include(a => a.BrandsModels.Model)
                   .FirstOrDefaultAsync();
        }


        public static void status(Vehicles Vehicles)
        {

            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            cruzRojaContext.Update(Vehicles);

            cruzRojaContext.SaveChanges();
        
        }


        public void CreateVehicle(Vehicles vehicles)
        {

            spaceCamelCase(vehicles);
            Create(vehicles);
        }

        private void spaceCamelCase(Vehicles vehicles)
        {
            vehicles.VehiclePatent = WithoutSpace_CamelCase.GetCamelCase(vehicles.VehiclePatent);
            vehicles.VehicleUtility = WithoutSpace_CamelCase.GetCamelCase(vehicles.VehiclePatent);
            vehicles.VehicleDescription = WithoutSpace_CamelCase.GetCamelCase(vehicles.VehiclePatent);

            if (vehicles.TypeVehicles != null)
            {
                vehicles.TypeVehicles.Type = WithoutSpace_CamelCase.GetCamelCase(vehicles.TypeVehicles.Type);
            }

            if (vehicles.BrandsModels != null)
            {
                vehicles.BrandsModels.Brands.BrandName = WithoutSpace_CamelCase.GetCamelCase(vehicles.BrandsModels.Brands.BrandName);
                vehicles.BrandsModels.Model.ModelName = WithoutSpace_CamelCase.GetCamelCase(vehicles.BrandsModels.Model.ModelName);
            }
        }

        public void UpdateVehicle(Vehicles vehicles)
        {
            Update(vehicles);
        }

        public void DeleteVehicle(Vehicles vehicles)
        {
            Delete(vehicles);
        }

        public IEnumerable<Vehicles> GetAllVehicles()
        {

            var vehicles = UsersRepository.authUser;

            var collection = _cruzRojaContext.Vehicles as IQueryable<Vehicles>;

            collection = collection.Where(
                                         a => a.Estates.Locations.LocationDepartmentName == vehicles.Estates.Locations.LocationDepartmentName);

            return collection
                   .Include(a => a.Estates)
                   .Include(a => a.Estates.LocationAddress)
                   .Include(a => a.Estates.EstatesTimes)
                   .ThenInclude(a => a.Times)
                   .ThenInclude(a => a.Schedules)
                   .Include(a => a.Employees)
                   .ThenInclude(a => a.Users)
                   .ThenInclude(a => a.Persons)
                   .Include(a => a.TypeVehicles)
                   .Include(a => a.Estates.Locations)
                   .Include(a => a.BrandsModels)
                   .Include(a => a.BrandsModels.Brands)
                   .Include(a => a.BrandsModels.Model)
                 .ToList();
        }
    }
}
