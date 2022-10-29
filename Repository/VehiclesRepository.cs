using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Helpers;
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
        public async Task<IEnumerable<Vehicles>> GetAllVehiclesFilters(int userId, int locationId)
        {
            var user =  EmployeesRepository.GetAllEmployeesById(userId);

            var collection = _cruzRojaContext.Vehicles as IQueryable<Vehicles>;

            if (!locationId.Equals(0))
                collection = collection.Where
                                            (a => a.Estates.Locations.LocationID.Equals(locationId));
            else
                return await GetAllVehicles();

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
                      .Include(a => a.Brands)
                      .Include(a => a.Model)

                      .Include(a => a.EmployeeCreated)
                      .ThenInclude(i => i.Users)
                      .ThenInclude(i => i.Persons)
                      .Include(i => i.EmployeeCreated.Users.Roles)

                      .Include(i => i.EmployeeModified)
                      .ThenInclude(i => i.Users)
                      .ThenInclude(i => i.Persons)
                      .Include(i => i.EmployeeModified.Users.Roles)
                      .ToListAsync();
        }

        public async Task<Vehicles> GetVehicleById(string vehicleId)
        {
            return await FindByCondition(vehicle => vehicle.ID == vehicleId)
                .FirstOrDefaultAsync();
        }

        public async Task<Vehicles> GetVehicleWithDetails(string vehicleId)
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
                      .Include(a => a.Brands)
                      .Include(a => a.Model)

                      .Include(a => a.EmployeeCreated)
                      .ThenInclude(i => i.Users)
                      .ThenInclude(i => i.Persons)
                      .Include(i => i.EmployeeCreated.Users.Roles)

                      .Include(i => i.EmployeeModified)
                      .ThenInclude(i => i.Users)
                      .ThenInclude(i => i.Persons)
                      .Include(i => i.EmployeeModified.Users.Roles)
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
            //spaceCamelCase(vehicles);
            Create(vehicles);
        }

        private void spaceCamelCase(Vehicles vehicles)
        {
            vehicles.VehiclePatent = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(vehicles.VehiclePatent);
            vehicles.VehicleUtility = WithoutSpace_CamelCase.GetWithoutSpace(vehicles.VehicleUtility);
            vehicles.VehicleDescription = WithoutSpace_CamelCase.GetWithoutSpace(vehicles.VehicleDescription);

            if (vehicles.TypeVehicles != null)
            {
                vehicles.TypeVehicles.Type = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(vehicles.TypeVehicles.Type);
            }

          /*  if (vehicles.BrandsModels.Brands != null || vehicles.BrandsModels.Model != null)
            {
                vehicles.BrandsModels.Brands.BrandName = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(vehicles.BrandsModels.Brands.BrandName);
                vehicles.BrandsModels.Model.ModelName = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(vehicles.BrandsModels.Model.ModelName);
            }*/
        }

        public void UpdateVehicle(Vehicles vehicles)
        {
            Update(vehicles);
        }

        public void DeleteVehicle(Vehicles vehicles)
        {
            Delete(vehicles);
        }

        public IEnumerable<Vehicles> GetAllVehicles(int userId)
        {

            var user = EmployeesRepository.GetAllEmployeesById(userId);

            var collection = _cruzRojaContext.Vehicles as IQueryable<Vehicles>;

            collection = collection.Where(
                                         a => a.Estates.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);

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
                   .Include(a => a.Brands)
                   .Include(a => a.Model)

                   .Include(a => a.EmployeeCreated)
                   .ThenInclude(i => i.Users)
                   .ThenInclude(i => i.Persons)
                   .Include(i => i.EmployeeCreated.Users.Roles)

                   .Include(i => i.EmployeeModified)
                   .ThenInclude(i => i.Users)
                   .ThenInclude(i => i.Persons)
                   .Include(i => i.EmployeeModified.Users.Roles)

                 .ToList();
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
                      .Include(a => a.Estates.Locations)
                      .Include(a => a.Brands)
                      .Include(a => a.Model)

                      .Include(a => a.EmployeeCreated)
                      .ThenInclude(i => i.Users)
                      .ThenInclude(i => i.Persons)
                      .Include(i => i.EmployeeCreated.Users.Roles)

                      .Include(i => i.EmployeeModified)
                      .ThenInclude(i => i.Users)
                      .ThenInclude(i => i.Persons)
                      .Include(i => i.EmployeeModified.Users.Roles)
                      .ToListAsync();
        }

        public IEnumerable<Vehicles> GetAllVehicles(DateTime dateStart, DateTime dateEnd, int estateId)
        {
            var user = EmployeesRepository.GetAllEmployeesById(estateId);


            var collection = _cruzRojaContext.Vehicles as IQueryable<Vehicles>;
            var fecha = Convert.ToDateTime("01/01/0001");

            if (dateEnd == fecha)
            {
                collection = collection.Where(
                                             a => a.VehicleDateCreated >= dateStart 
                                             && a.VehicleAvailability == false
                                             && a.FK_EstateID == estateId);
            }
            else
            {
                collection = collection.Where(
                                         a => a.VehicleDateCreated >= dateStart 
                                         && a.VehicleDateCreated <= dateEnd
                                         && a.VehicleAvailability == false
                                         && a.FK_EstateID == user.FK_EstateID);
            }

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
                   .Include(a => a.Brands)
                   .Include(a => a.Model)
                   .Include(a => a.EmployeeCreated)
                   .ThenInclude(i => i.Users)
                   .ThenInclude(i => i.Persons)
                   .Include(i => i.EmployeeCreated.Users.Roles)
                   .Include(i => i.EmployeeModified)
                   .ThenInclude(i => i.Users)
                   .ThenInclude(i => i.Persons)
                   .Include(i => i.EmployeeModified.Users.Roles)
                   .ToList();
        }
    }
}
