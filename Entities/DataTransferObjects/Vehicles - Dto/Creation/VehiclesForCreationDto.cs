using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Vehicles___Dto.Creation
{
    public class VehiclesForCreationDto
    {
        public string VehiclePatent { get; set; }

        public string VehicleUtility { get; set; }

        public string VehicleDescription { get; set; }

        public Boolean VehicleAvailability { get; set; }
        public string VehiclePicture { get; set; }

        public int FK_EstateID { get; set; }
        public int FK_EmployeeID { get; set; }
        public int Fk_TypeVehicleID { get; set; }
    }
}
