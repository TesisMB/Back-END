using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Materials___Dto
{
   public class MaterialsForCreationDto
    {
        public string MaterialName { get; set; }
        
        public string MaterialMark { get; set; }

        public int MaterialQuantity { get; set; }

        public string MaterialUtility { get; set; }

        public Boolean MaterialAvailability { get; set; } = true;

        public string MaterialPicture { get; set; }
        public int FK_EstateID { get; set; }
    }
}
