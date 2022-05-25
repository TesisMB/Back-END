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
        public async Task<IEnumerable<Materials>> GetAllMaterials(int userId)
        {
            //var material = UsersRepository.authUser;

            var user =  EmployeesRepository.GetAllEmployeesById(userId);


            var collection = _cruzRojaContext.Materials as IQueryable<Materials>;


            collection = collection.Where(
                a => a.Estates.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);



            return await collection
                       .Include(a => a.Estates)
                       .Include(a => a.Estates.LocationAddress)
                       .Include(a => a.Estates.EstatesTimes)
                       .ThenInclude(a => a.Times)
                       .ThenInclude(a => a.Schedules)
                       .Include(a => a.Estates.Locations)

                        .Include(a => a.EmployeeCreated)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeCreated.Users.Roles)

                        .Include(i => i.EmployeeModified)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeModified.Users.Roles)
                       .ToListAsync();

        }


        public static void status(Materials materials)
        {

            CruzRojaContext cruzRojaContext = new CruzRojaContext();
            materials.Resources_RequestResources_Materials_Medicines_Vehicles = null;

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

                       .Include(a => a.EmployeeCreated)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeCreated.Users.Roles)

                        .Include(i => i.EmployeeModified)
                        .ThenInclude(i => i.Users)
                        .ThenInclude(i => i.Persons)
                        .Include(i => i.EmployeeModified.Users.Roles)
                       .FirstOrDefaultAsync();
        }

        public void CreateMaterial(Materials material)
        {
            //spaceCamelCase(material);

            Create(material);
        }

        private void spaceCamelCase(Materials material)
        {
            material.MaterialName = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(material.MaterialName);
            material.MaterialBrand = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(material.MaterialBrand);
            material.MaterialUtility = WithoutSpace_CamelCase.GetWithoutSpace(material.MaterialUtility);
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
