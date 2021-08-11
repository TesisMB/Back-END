using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public class EstatesRepository : RepositoryBase<Estates>, IEstatesRepository                                                                                                                            
    {
        public EstatesRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {

        }

        public async Task<IEnumerable<Estates>> GetAllEstates()
        {
            return await FindAll()
                .Include(i => i.LocationAddress)
                .Include(i => i.EstatesTimes)
                .ThenInclude(i => i.Times)
                .ThenInclude(i => i.Schedules)
                .ToListAsync();
        }

        public async Task<IEnumerable<Estates>> GetAllEstatesType()
        {
            return await FindAll()
                          .ToListAsync();
        }
    }
}
