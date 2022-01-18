using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class EmergenciesDisastersRepository : RepositoryBase<EmergenciesDisasters>, IEmergenciesDisastersRepository
    {
        public EmergenciesDisastersRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {

        }


        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisasters()
        {
            return await FindAll()
                .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.Alerts)
                .Include(i => i.Locations)
                .Include(i => i.Employees)
                .ThenInclude(i => i.Users)
                .ThenInclude(i => i.Persons)
                .Include(i => i.Employees.Users.Roles)
                .ToListAsync();

        }

        public async Task<EmergenciesDisasters> GetEmergencyDisasterWithDetails(int emergencydisasterId)
        {
            return await FindByCondition(emergdis => emergdis.EmergencyDisasterID.Equals(emergencydisasterId))
           .Include(i => i.TypesEmergenciesDisasters)
           .Include(i => i.Alerts)
           .Include(i => i.Locations)
           .Include(i => i.Employees)
           .ThenInclude(i => i.Users)
           .ThenInclude(i => i.Persons)
           .Include(i => i.Employees.Users.Roles)
           .FirstOrDefaultAsync();
        }


        public async Task<EmergenciesDisasters> GetEmergencyDisasterById(int emergencydisasterId)
        {
            return await FindByCondition(emergdis => emergdis.EmergencyDisasterID.Equals(emergencydisasterId))
            .FirstOrDefaultAsync();

        }
        public void CreateEmergencyDisaster(EmergenciesDisasters emergencyDisaster)
        {
            spaceCamelCase(emergencyDisaster);

            Create(emergencyDisaster);
        }

        private void spaceCamelCase(EmergenciesDisasters emergencyDisaster)
        {
            if(emergencyDisaster.EmergencyDisasterInstruction != null)
            {
            emergencyDisaster.EmergencyDisasterInstruction = WithoutSpace_CamelCase.GetCamelCase(emergencyDisaster.EmergencyDisasterInstruction);
            }
        }

        public void UpdateEmergencyDisaster(EmergenciesDisasters emergencyDisaster)
        {
            spaceCamelCase(emergencyDisaster);

            Update(emergencyDisaster);
        }

        public void DeleteEmergencyDisaster(EmergenciesDisasters emergencyDisaster)
        {
            Delete(emergencyDisaster);
        }
    }
}
