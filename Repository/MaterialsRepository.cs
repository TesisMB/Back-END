using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class MaterialsRepository : RepositoryBase<Materials>, IMaterialsRepository
    {
        private CruzRojaContext _cruzRojaContext;

        public MaterialsRepository(CruzRojaContext cruzRojaContext)
        : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }
        public async Task<IEnumerable<Materials>> GetAllMaterials()
        {
            var material = UsersRepository.authUser;


            var collection = _cruzRojaContext.Materials as IQueryable<Materials>;


            collection = collection.Where(
                a => a.Estates.Locations.LocationDepartmentName == material.Estates.Locations.LocationDepartmentName);



            return await collection
                       .Include(a => a.Estates)
                       .Include(a => a.Estates.LocationAddress)
                       .Include(a => a.Estates.EstatesTimes)
                       .ThenInclude(a => a.Times)
                       .ThenInclude(a => a.Schedules)
                       .Include(a => a.Estates.Locations)
                       .ToListAsync();

        }


        public static void status(Materials materials)
        {

            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            cruzRojaContext.Update(materials);

            cruzRojaContext.SaveChanges();
        }

        public async Task<Materials> GetMaterialById(string materialId)
        {
            return await FindByCondition(material => material.ID == materialId)
                           .FirstOrDefaultAsync();
        }

        public async Task<Materials> GetMaterialWithDetails(string materialId)
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
            spaceCamelCase(material);

            Create(material);
        }

        private void spaceCamelCase(Materials material)
        {
            material.MaterialName = WithoutSpace_CamelCase.GetCamelCase(material.MaterialName);
            material.MaterialBrand = WithoutSpace_CamelCase.GetCamelCase(material.MaterialBrand);
            material.MaterialUtility = WithoutSpace_CamelCase.GetCamelCase(material.MaterialUtility);
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
