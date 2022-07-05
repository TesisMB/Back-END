using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EmergenciesDisastersRepository : RepositoryBase<EmergenciesDisasters>, IEmergenciesDisastersRepository
    {
        private readonly CruzRojaContext _cruzRojaContext = new CruzRojaContext();

        public EmergenciesDisastersRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;

        }


        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisasters(int userId)
        {

            //var user = UsersRepository.authUser;
            var user = EmployeesRepository.GetAllEmployeesById(userId);


            var collection = _cruzRojaContext.EmergenciesDisasters as IQueryable<EmergenciesDisasters>;

            if (user.Roles.RoleName != "Coordinador General" && user.Roles.RoleName != "Admin")
            {
                return await GetAllEmergenciesDisastersFilter(userId);
            }
            else
            {
                collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID && a.EmergencyDisasterEndDate == null);
            }

            return await collection
                .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.LocationsEmergenciesDisasters)
                 .Include(a => a.EmployeeModified)
                 .OrderBy(a => a.EmergencyDisasterStartDate)
                .ToListAsync();
        }



        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersWithourFilter(int userId, string limit)
        {
            //var user = UsersRepository.authUser;

            var user =  EmployeesRepository.GetAllEmployeesById(userId);

            var collection = _cruzRojaContext.EmergenciesDisasters as IQueryable<EmergenciesDisasters>;
            if (string.IsNullOrEmpty(limit))
            {
                collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID);

            }
            else {

                collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID)
                    .OrderByDescending(a => a.EmergencyDisasterID)
                    .Take(2)
                    .AsNoTracking();

            }


            //Falta filtrar unicamente los recursos solamente aceptados

            return await collection
        .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.Alerts)
                .Include(i => i.LocationsEmergenciesDisasters)
                .Include(i => i.EmployeeIncharge)
                .ThenInclude(i => i.Users)
                .ThenInclude(i => i.Persons)
                .Include(i => i.EmployeeIncharge.Users.Roles)

                .Include(i => i.Resources_Requests)
                .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles)

                 .ThenInclude(a => a.Model)

                  .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles)


                 .ThenInclude(a => a.Brands)

                     .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles.TypeVehicles)


                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Medicines)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.UsersChatRooms)
                 .ThenInclude(a => a.Users)
                 .ThenInclude(a => a.Persons)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.UsersChatRooms)
                 .ThenInclude(a => a.Users)
                 .ThenInclude(a => a.Roles)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.UsersChatRooms)
                 .ThenInclude(a => a.Users.Volunteers)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.Messages)

                 .Include(a => a.Victims)

                 .Include(a => a.VolunteersLocationVolunteersEmergenciesDisasters)

                  .Include(i => i.EmployeeModified)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeModified.Users.Roles)

                 .Include(i => i.EmployeeCreated)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeCreated.Users.Roles)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.EmployeeCreated)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeCreated.Users.Roles)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.EmployeeModified)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeModified.Users.Roles)
                .ToListAsync();

        }


        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersWithourFilterApp()
        {

            var user = UsersRepository.authUser;

            var collection = _cruzRojaContext.EmergenciesDisasters as IQueryable<EmergenciesDisasters>;

            //Falta filtrar unicamente los recursos solamente aceptados
            collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID);

            return await collection
                .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.Alerts)
                .Include(i => i.LocationsEmergenciesDisasters)
                .Include(i => i.EmployeeIncharge)
                .ThenInclude(i => i.Users)
                .ThenInclude(i => i.Persons)
                .Include(i => i.EmployeeIncharge.Users.Roles)


                 .Include(a => a.ChatRooms)
                 .Include(a => a.ChatRooms.UsersChatRooms)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)

                 .Include(a => a.ChatRooms)
                 .Include(a => a.ChatRooms.UsersChatRooms)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Roles)

                 .OrderBy(i => i.EmergencyDisasterStartDate)
                .ToListAsync();

        }


        public async Task<EmergenciesDisasters> GetEmergencyDisasterWithDetails(int emergencydisasterId)
        {
            return await FindByCondition(emergdis => emergdis.EmergencyDisasterID.Equals(emergencydisasterId))
        
                .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.Alerts)
                .Include(i => i.LocationsEmergenciesDisasters)
                .Include(i => i.EmployeeIncharge)
                .ThenInclude(i => i.Users)
                .ThenInclude(i => i.Persons)
                .Include(i => i.EmployeeIncharge.Users.Roles)

                .Include(i => i.Resources_Requests)
                .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles)

                 .ThenInclude(a => a.Model)

                  .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles)


                 .ThenInclude(a => a.Brands)

                     .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles.TypeVehicles)


                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Medicines)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.UsersChatRooms)
                 .ThenInclude(a => a.Users)
                 .ThenInclude(a => a.Persons)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.UsersChatRooms)
                 .ThenInclude(a => a.Users)
                 .ThenInclude(a => a.Roles )

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.UsersChatRooms)
                 .ThenInclude(a => a.Users.Volunteers)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.Messages)

                 .Include(a => a.Victims)

                 .Include(a => a.VolunteersLocationVolunteersEmergenciesDisasters)

                  .Include(i => i.EmployeeModified)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeModified.Users.Roles)

                 .Include(i => i.EmployeeCreated)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeCreated.Users.Roles)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.EmployeeCreated)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeCreated.Users.Roles)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.EmployeeModified)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeModified.Users.Roles)

           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersFilter(int userId)
        {
            var user = EmployeesRepository.GetAllEmployeesById(userId);

            //var user = UsersRepository.authUser;
            var collection = _cruzRojaContext.EmergenciesDisasters as IQueryable<EmergenciesDisasters>;

            collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID && a.EmergencyDisasterEndDate == null);


            return await collection
                 .Include(i => i.TypesEmergenciesDisasters)
                 .Include(i => i.Alerts)
                 .Include(i => i.LocationsEmergenciesDisasters)
                 .Include(i => i.EmployeeIncharge)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeIncharge.Users.Roles)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Materials)

                  .Include(i => i.Resources_Requests)
                  .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                  .ThenInclude(i => i.Vehicles)

                  .ThenInclude(a => a.Model)

                  .Include(i => i.Resources_Requests)
                  .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                  .ThenInclude(i => i.Vehicles)


                  .ThenInclude(a => a.Brands)

                   .Include(i => i.Resources_Requests)
                  .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                  .ThenInclude(i => i.Vehicles.TypeVehicles)


                  .Include(i => i.Resources_Requests)
                  .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                  .ThenInclude(i => i.Medicines)

                  .Include(i => i.EmployeeModified)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeModified.Users.Roles)

                 .ToListAsync();
        }


        public async Task<EmergenciesDisasters> GetEmergencyDisasterById(int emergencydisasterId)
        {
            return await FindByCondition(emergdis => emergdis.EmergencyDisasterID.Equals(emergencydisasterId))
                  .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.Alerts)
                .Include(i => i.LocationsEmergenciesDisasters)
                .Include(i => i.EmployeeIncharge)
                .ThenInclude(i => i.Users)
                .ThenInclude(i => i.Persons)
                .Include(i => i.EmployeeIncharge.Users.Roles)

                .Include(i => i.Resources_Requests)
                .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles)

                 .ThenInclude(a => a.Model)

                  .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles)


                 .ThenInclude(a => a.Brands)

                     .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles.TypeVehicles)


                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Medicines)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.UsersChatRooms)
                 .ThenInclude(a => a.Users)
                 .ThenInclude(a => a.Persons)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.UsersChatRooms)
                 .ThenInclude(a => a.Users)
                 .ThenInclude(a => a.Roles)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.UsersChatRooms)
                 .ThenInclude(a => a.Users.Volunteers)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.Messages)

                 .Include(a => a.Victims)

                 .Include(a => a.VolunteersLocationVolunteersEmergenciesDisasters)

                 .Include(a => a.PDF)

            .FirstOrDefaultAsync();

        }
        public void CreateEmergencyDisaster(EmergenciesDisasters emergencyDisaster)
        {
            SpaceCamelCase(emergencyDisaster);

            Create(emergencyDisaster);
        }

        private void SpaceCamelCase(EmergenciesDisasters emergencyDisaster)
        {
            if (emergencyDisaster.EmergencyDisasterInstruction != null)
            {
                emergencyDisaster.EmergencyDisasterInstruction = WithoutSpace_CamelCase.GetWithoutSpace(emergencyDisaster.EmergencyDisasterInstruction);
            }
        }

        public void UpdateEmergencyDisaster(EmergenciesDisasters emergencyDisaster)
        {
            SpaceCamelCase(emergencyDisaster);

            Update(emergencyDisaster);
        }

        public void DeleteEmergencyDisaster(EmergenciesDisasters emergencyDisaster)
        {
            Delete(emergencyDisaster);
        }
    }
}
