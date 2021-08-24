using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmergenciesDisastersRepository: RepositoryBase<EmergenciesDisasters>, IEmergenciesDisastersRepository
    {
        public EmergenciesDisastersRepository(CruzRojaContext cruzRojaContext): base(cruzRojaContext)
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
            Create(emergencyDisaster);
        }


        public void UpdateEmergencyDisaster(EmergenciesDisasters emergencyDisaster)
        {
            Update(emergencyDisaster);
        }

        public void DeleteEmergencyDisaster(EmergenciesDisasters emergencyDisaster)
        {
            Delete(emergencyDisaster);
        }
    }
}
