using Entities.Models;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.BrandsModels__Dto
{
    public class BrandsModelsForSelectDto
    {
        public int FK_TypeVehicleID { get; set; }

        public string Type { get; set; }



        public ICollection<BrandsModelsDto> BrandModels { get; set; }
    }

    public class TypesSelect
    {
        public int TypeID { get; set; }

        public string Type { get; set; }
    }

    public class BrandsModelsDto
    {
        public int FK_BrandID { get; set; }
        public string BrandsName { get; set; }

        public int FK_ModelID { get; set; }
        public string ModelName { get; set; }
    }

    public class ModelsSelect
    {
        public int ModelID { get; set; }
        public string ModelName { get; set; }
    }

}
