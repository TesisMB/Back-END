using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
   public class MaterialsRepository : RepositoryBase<Materials>, IMaterialsRepository
    {
        public MaterialsRepository(CruzRojaContext cruzRojaContext2)
        : base(cruzRojaContext2)
        {

        }
        public IEnumerable<Materials> GetAllMaterials()
        {
            return FindAll()
                       .Include(a => a.Estates)
                       .Include(a => a.Estates.LocationAddress)
                       .Include(a => a.Estates.EstatesTimes)
                       .ThenInclude(a => a.Times)
                       .ThenInclude(a => a.Schedules)
                       .ToList();

        }

        public Materials GetMaterialById(int materialId)
        {
            return FindByCondition(material => material.MaterialID == materialId)
                           .FirstOrDefault();
        }

        public Materials GetMaterialWithDetails(int materialId)
        {
            return FindByCondition(material => material.MaterialID == materialId)
                       .Include(a => a.Estates)
                       .Include(a => a.Estates.LocationAddress)
                       .Include(a => a.Estates.EstatesTimes)
                       .ThenInclude(a => a.Times)
                       .ThenInclude(a => a.Schedules)
                       .FirstOrDefault();
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
