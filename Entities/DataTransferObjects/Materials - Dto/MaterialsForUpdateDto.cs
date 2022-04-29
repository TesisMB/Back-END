using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Materials___Dto
{
    public class MaterialsForUpdateDto
    {
        public string MaterialName { get; set; }
        public int MaterialQuantity { get; set; }
        public Boolean MaterialAvailability { get; set; }
        public Boolean MaterialDonation { get; set; }

        public string MedicinePicture { get; set; }
        public int FK_EstateID { get; set; }
        public string Description { get; set; }

        public string MaterialBrand { get; set; }
    }
}
