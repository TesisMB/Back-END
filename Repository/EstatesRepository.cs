using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
   public class EstatesRepository : RepositoryBase<Estates>, IEstatesRepository                                                                                                                            
    {
        public EstatesRepository(CruzRojaContext repositoryContext): base(repositoryContext)
        {

        }



        IEnumerable<Estates> IEstatesRepository.GetAllEstates()
        {
            return FindAll()
                .Include(i => i.LocationAddress)
                .Include(i => i.EstatesTimes)
                .ThenInclude(i => i.Times)
                .ThenInclude(i => i.Schedules)
                .ToList();
        }
    }
}
