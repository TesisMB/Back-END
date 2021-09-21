using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class Resources_RequestRepository: RepositoryBase<Resources_Request>, IResources_RequestRepository
    {
        private CruzRojaContext _cruzRojaContext;
        public Resources_RequestRepository(CruzRojaContext cruzRojaContext) :base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }


        public async Task<IEnumerable<Resources_Request>> GetAllResourcesRequest()
        {
            var user = UsersRepository.authUser;

            var collection = _cruzRojaContext.Resources_Requests as IQueryable<Resources_Request>;

            if(user.Roles.RoleName == "Encargado de Logistica")
            {
                collection = collection.Where(
                    a => a.Users.Estates.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);
            }
            else
            {
                return null;
            }

            return await collection
                .Include(i => i.EmergenciesDisasters)
                .Include(i => i.EmergenciesDisasters.TypesEmergenciesDisasters)
                .Include(i => i.EmergenciesDisasters.Alerts)
                .Include(i => i.Users)
                .Include(i => i.Users.Persons)
                .Include(i => i.Users.Roles)
                .Include(i => i.Users.Estates)
                .ThenInclude(i => i.Locations)
                .Include(i => i.Users.Locations)
                .Include(i => i.Resources)
                .Include(i => i.Resources.Resources_Medicines)
                .ThenInclude(i => i.Medicines)
                .Include(i => i.Resources.Resources_Materials)
                .ThenInclude(i => i.Materials)
                .Include(i => i.Resources.Resources_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ToListAsync();
        }
        public void CreateResource_Resquest(Resources_Request resources_Request)
        {
            Create(resources_Request);
        }
    }
}
