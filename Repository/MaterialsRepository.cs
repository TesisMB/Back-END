using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public class MaterialsRepository : RepositoryBase<Materials>, IMaterialsRepository
    {
        public MaterialsRepository(CruzRojaContext cruzRojaContext)
        : base(cruzRojaContext)
        {

        }
        public async Task<IEnumerable<Materials>> GetAllMaterials()
        {
            return await FindAll()
                       .Include(a => a.Estates)
                       .Include(a => a.Estates.LocationAddress)
                       .Include(a => a.Estates.EstatesTimes)
                       .ThenInclude(a => a.Times)
                       .ThenInclude(a => a.Schedules)
                       .Include(a => a.Estates.Locations)
                       .ToListAsync();

        }

        public async Task<Materials> GetMaterialById(int materialId)
        {
            return await FindByCondition(material => material.ID == materialId)
                           .FirstOrDefaultAsync();
        }

        public async Task<Materials> GetMaterialWithDetails(int materialId)
        {
            return await FindByCondition(material => material.ID == materialId)
                       .Include(a => a.Estates)
                       .Include(a => a.Estates.LocationAddress)
                       .Include(a => a.Estates.EstatesTimes)
                       .ThenInclude(a => a.Times)
                       .ThenInclude(a => a.Schedules)
                       .Include(a => a.Estates.Locations)
                       .FirstOrDefaultAsync();
        }

        public void CreateMaterial(Materials material)
        {
            Create(material);
        }


        public void UpdateMaterial(Materials material)
        {
            Update(material);
        }

        public void DeleteMaterial(Materials material)
        {
            Delete(material);
        }

    }
}
