using Entities.DataTransferObjects.BrandsModels__Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    public class TypesSelectComparer : IEqualityComparer<TypesSelect>
    {
        public bool Equals(TypesSelect x, TypesSelect y)
        {
            return x.TypeID == y.TypeID;
        }

        public int GetHashCode(TypesSelect obj)
        {
            return obj.TypeID.GetHashCode();
        }
    }
}