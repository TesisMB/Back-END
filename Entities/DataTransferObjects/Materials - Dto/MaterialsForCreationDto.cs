using System;

namespace Entities.DataTransferObjects.Materials___Dto
{
    public class MaterialsForCreationDto
    {
        public string MaterialName { get; set; }

        public string MaterialBrand { get; set; }

        public int MaterialQuantity { get; set; }

        public string MaterialUtility { get; set; }

        public Boolean MaterialAvailability { get; set; } = true;

        public string MaterialPicture { get; set; }
        public int FK_EstateID { get; set; }
    }
}
