using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Materials___Dto
{
    public class MaterialsForUpdateDto
    {
        public string MaterialQuantity { get; set; }

        public Boolean MaterialAvailability { get; set; }
        public string MaterialPicture { get; set; }
        public int FK_EstateID { get; set; }
    }
}
