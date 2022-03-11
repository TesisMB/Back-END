using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class EstatesRepository : RepositoryBase<Locations>, IEstatesRepository
    {
        public EstatesRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {

        }

        public async Task<IEnumerable<Locations>> GetAllEstates()
        {
            return await FindAll()
                .Include(i => i.Estates)
                .ThenInclude(i => i.EstatesTimes)
                .ThenInclude(i => i.Times)
                .ThenInclude(i => i.Schedules)
                .Include(i => i.Estates)
                .ThenInclude(i => i.LocationAddress)
                .ToListAsync();
        }

        public async Task<IEnumerable<Locations>> GetAllEstatesType()
        {
            return await FindAll()
                          .ToListAsync();
        }
    }
}
