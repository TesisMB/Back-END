using System;

namespace Entities.DataTransferObjects.Materials___Dto
{
    public class MaterialsForUpdateDto
    {

        public string ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Boolean Availability { get; set; }
        public string Picture { get; set; }

        public string Description { get; set; }

        public int FK_EstateID { get; set; }

        public Boolean Donation { get; set; }

        public DateTime DateModified { get; set; } = DateTime.Now;

        public int ModifiedBy { get; set; }
        public bool Enabled { get; set; }
        public string reason { get; set; }

        public MaterialsDto Materials { get; set; }


    }
}
