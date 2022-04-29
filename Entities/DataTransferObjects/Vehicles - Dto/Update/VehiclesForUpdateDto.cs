using System;

namespace Entities.DataTransferObjects.Vehicles___Dto.Update
{
    public class VehiclesForUpdateDto
    {
        public string VehicleUtility { get; set; }
        public string VehiclePatent { get; set; }
        public string VehicleYear { get; set; }

        public string VehicleDescription { get; set; }
        public string VehiclePicture { get; set; }

        public Boolean VehicleAvailability { get; set; }
        public Boolean VehicleDonation { get; set; }

        public int FK_EstateID { get; set; }

        public int? FK_EmployeeID { get; set; }
    }
}
