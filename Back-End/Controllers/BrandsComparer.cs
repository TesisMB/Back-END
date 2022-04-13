using Entities.DataTransferObjects.BrandsModels__Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    public class BrandsComparer : IEqualityComparer<BrandsSelect>
    {
        public bool Equals(BrandsSelect x, BrandsSelect y)
        {
            return x.BrandID == y.BrandID;
        }

        public int GetHashCode(BrandsSelect obj)
        {
            return obj.BrandID.GetHashCode();
        }
    }
}