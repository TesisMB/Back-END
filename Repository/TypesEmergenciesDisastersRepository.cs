using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public class TypesEmergenciesDisastersRepository : RepositoryBase<TypesEmergenciesDisasters>, ITypesEmergenciesDisastersRepository
    {
        public TypesEmergenciesDisastersRepository(CruzRojaContext cruzRojaContext):base(cruzRojaContext)
        {

        }
        public async Task<IEnumerable<TypesEmergenciesDisasters>> GetAllTypesEmergenciesDisasters()
        {
            return await FindAll()
                .ToListAsync();
        }
    }
}
