using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class TypesVehiclesRepository : RepositoryBase<BrandsModels>, ITypesVehicles
    {
        public TypesVehiclesRepository(CruzRojaContext cruzRojaContext): base(cruzRojaContext)
        {

        }
        public async Task<IEnumerable<BrandsModels>> GetAllTypesVehicles()
        {

            return await FindAll()
            .Include(a => a.Vehicles)
            .ThenInclude(a => a.TypeVehicles)
            .Include(a => a.Brands)
            .Include(a => a.Model)
            .ToListAsync();
        }
    }
}
