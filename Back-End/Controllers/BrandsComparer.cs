using Entities.DataTransferObjects.BrandsModels__Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    public class BrandsComparer : IEqualityComparer<BrandsModelsDto>
    {
        public bool Equals(BrandsModelsDto x, BrandsModelsDto y)
        {
            return x.FK_BrandID == y.FK_BrandID;
        }

        public int GetHashCode(BrandsModelsDto obj)
        {
            return obj.FK_BrandID.GetHashCode();
        }
    }
}