using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.BrandsModels__Dto;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class TypesVehiclesRepository : RepositoryBase<TypeVehicles>, ITypesVehicles
    {
        private CruzRojaContext _cruzRojaContext;

        public TypesVehiclesRepository(CruzRojaContext cruzRojaContext): base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;

        }
        public async Task<IEnumerable<TypeVehicles>> GetAllTypesVehicles()
        {

            return await FindAll()
         //   .Include(a => a.Vehicles)
         //   .ThenInclude(a => a.TypeVehicles)
            .Include(a => a.BrandModels)
            .ThenInclude(a => a.Brands)


            .Include(a => a.BrandModels)
            .ThenInclude(a => a.Model)
            .ToListAsync();
        }

      /*  public async Task<IEnumerable<BrandsModels>> GetFilterTypesVehicles(BrandsModelsForSelectDto brandsModels)
        {
            var collection = _cruzRojaContext.BrandsModels as IQueryable<BrandsModels>;


            if (string.IsNullOrEmpty(brandsModels.BrandsName) 
                && (string.IsNullOrEmpty(brandsModels.ModelName)
                && (string.IsNullOrEmpty(brandsModels.Type))))
            {
                return await GetAllTypesVehicles();
            }


            if (!string.IsNullOrEmpty(brandsModels.Type))
            {
                TypeVehicles typeVehicles = null;

                typeVehicles = _cruzRojaContext.TypeVehicles.Where(a => a.Type == brandsModels.Type)
                                .AsNoTracking()
                                .FirstOrDefault();

                    foreach (var item in typeVehicles.Vehicles)
                    {
                         List<string> Key = new List<string>();

                        Key.Add(item.BrandsModels.Brands.BrandName);

                    }


                    }

            else if (string.IsNullOrEmpty(brandsModels.BrandsName))

            {
                    collection = collection.Where(a => a.Model.ModelName == brandsModels.ModelName);
            }


            return await collection
                .Include(a => a.Vehicles)
                .ThenInclude(a => a.TypeVehicles)
                .Include(a => a.Brands)
                .Include(a => a.Model)
                .ToListAsync();


        }*/

    }
}
