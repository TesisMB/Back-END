using Entities.DataTransferObjects.BrandsModels__Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    public class ModelsComparer : IEqualityComparer<ModelsSelect>
    {
        public bool Equals(ModelsSelect x, ModelsSelect y)
        {
            return x.ModelID == y.ModelID;
        }

        public int GetHashCode(ModelsSelect obj)
        {
            return obj.ModelID.GetHashCode();
        }
    }
}