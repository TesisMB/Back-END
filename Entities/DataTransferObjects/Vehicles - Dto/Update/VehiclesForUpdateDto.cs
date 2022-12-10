using Entities.DataTransferObjects.Vehicles___Dto.Creation;
using System;

namespace Entities.DataTransferObjects.Vehicles___Dto.Update
{
    public class VehiclesForUpdateDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Boolean Availability { get; set; }
        public string Picture { get; set; }

        public string Description { get; set; }

        public int FK_EstateID { get; set; }
        public bool Enabled { get; set; }

        public Boolean Donation { get; set; }

        public DateTime? DateModified { get; set; }

        public int ModifiedBy { get; set; }

        public VehiclesForCreationDto Vehicles { get; set; }

    }
}
