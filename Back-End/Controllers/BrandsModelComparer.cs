﻿using Entities.DataTransferObjects.BrandsModels__Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    public class BrandsModelComparer : IEqualityComparer<BrandsModelsForSelectDto>
    {
        public bool Equals(BrandsModelsForSelectDto x, BrandsModelsForSelectDto y)
        {
            return x.FK_TypeVehicleID == y.FK_TypeVehicleID;
        }

        public int GetHashCode(BrandsModelsForSelectDto obj)
        {
            return obj.FK_TypeVehicleID.GetHashCode();
        }
    }
}