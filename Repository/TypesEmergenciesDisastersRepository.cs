using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class TypesEmergenciesDisastersRepository : RepositoryBase<TypesEmergenciesDisasters>, ITypesEmergenciesDisastersRepository
    {
        public TypesEmergenciesDisastersRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {

        }
        public async Task<IEnumerable<TypesEmergenciesDisasters>> GetAllTypesEmergenciesDisasters()
        {
            return await FindAll()
                .ToListAsync();
        }

        public async Task<TypesEmergenciesDisasters> GetTypeEmergencyDisaster(int TypeEmergencyDisasterId)
        {
            return await FindByCondition(a => a.TypeEmergencyDisasterID.Equals(TypeEmergencyDisasterId))
                .Include(i => i.EmergenciesDisasters)
                .ThenInclude(i => i.Alerts)
                .Include(i => i.EmergenciesDisasters)
                .ThenInclude(i => i.EmployeeIncharge)
                .Include(i => i.EmergenciesDisasters)
                .ThenInclude(i => i.LocationsEmergenciesDisasters)
                .FirstOrDefaultAsync();
        }
    }
}
