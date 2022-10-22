using System.Collections.Generic;

namespace Entities.DataTransferObjects.BrandsModels__Dto
{
    public class BrandsModelsForSelectDto
    {
        public int TypeID { get; set; }

        public string Type { get; set; }

        public BrandsSelect Brands { get; set; }
    }

    public class TypesSelect
    {
        public int TypeID { get; set; }

        public string Type { get; set; }
    }

    public class BrandsSelect
    {
        public int BrandID { get; set; }
        public string BrandsName { get; set; }
    }

    public class ModelsSelect
    {
        public int ModelID { get; set; }
        public string ModelName { get; set; }
    }

}
