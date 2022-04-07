using System.Collections.Generic;

namespace Entities.DataTransferObjects.BrandsModels__Dto
{
    public class BrandsModelsForSelectDto
    {
        public int ID { get; set; }

        public int BrandID { get; set; }
        public string BrandsName { get; set; }


        public ICollection<Models> Models { get; set; }

        public int TypeID { get; set; }

        public string Type { get; set; }
    }

    public class Models
    {
        public int ModelID { get; set; }
        public string ModelName { get; set; }
    }

}
