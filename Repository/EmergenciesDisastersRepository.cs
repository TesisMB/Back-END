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


        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisasters()
        {

            var user = UsersRepository.authUser;
            var collection = _cruzRojaContext.EmergenciesDisasters as IQueryable<EmergenciesDisasters>;

            if (user.Roles.RoleName != "Coordinador General" && user.Roles.RoleName != "Admin")
            {
                return await GetAllEmergenciesDisastersFilter();
            }
            else
            {
                collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID && a.EmergencyDisasterEndDate == null);
            }

            return await collection
                .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.Alerts)
                .Include(i => i.LocationsEmergenciesDisasters)
                .Include(i => i.Employees)
                .ThenInclude(i => i.Users)
                .ThenInclude(i => i.Persons)
                .Include(i => i.Employees.Users.Roles)

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


                 .OrderByDescending(a => a.EmergencyDisasterStartDate)

                .ToListAsync();

        }



        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersWithourFilter()
        {
            var user = UsersRepository.authUser;
           
            var collection = _cruzRojaContext.EmergenciesDisasters as IQueryable<EmergenciesDisasters>;

            //Falta filtrar unicamente los recursos solamente aceptados
            collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID);

            return await collection
                .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.Alerts)
                .Include(i => i.LocationsEmergenciesDisasters)
                .Include(i => i.Employees)
                .ThenInclude(i => i.Users)
                .ThenInclude(i => i.Persons)
                .Include(i => i.Employees.Users.Roles)

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

                 .Include(a => a.Victims)

                 .Include(a => a.ChatRooms)
                 .Include(a => a.ChatRooms.UsersChatRooms)

                 .OrderByDescending(i => i.Alerts.AlertID)

                .ToListAsync();

        }



        public async Task<EmergenciesDisasters> GetEmergencyDisasterWithDetails(int emergencydisasterId)
        {
            return await FindByCondition(emergdis => emergdis.EmergencyDisasterID.Equals(emergencydisasterId))
          .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.Alerts)
                .Include(i => i.LocationsEmergenciesDisasters)
                .Include(i => i.Employees)
                .ThenInclude(i => i.Users)
                .ThenInclude(i => i.Persons)
                .Include(i => i.Employees.Users.Roles)

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

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.Messages)

                 .Include(a => a.Victims)

                 .Include(a => a.VolunteersLocationVolunteersEmergenciesDisasters)

           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersFilter()
        {

            var user = UsersRepository.authUser;
            var collection = _cruzRojaContext.EmergenciesDisasters as IQueryable<EmergenciesDisasters>;

            collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID && a.EmergencyDisasterEndDate == null);


            return await collection
                 .Include(i => i.TypesEmergenciesDisasters)
                 .Include(i => i.Alerts)
                 .Include(i => i.LocationsEmergenciesDisasters)
                 .Include(i => i.Employees)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.Employees.Users.Roles)

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



                 .ToListAsync();
        }


        public async Task<EmergenciesDisasters> GetEmergencyDisasterById(int emergencydisasterId)
        {
            return await FindByCondition(emergdis => emergdis.EmergencyDisasterID.Equals(emergencydisasterId))
           .Include(i => i.Victims)
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
