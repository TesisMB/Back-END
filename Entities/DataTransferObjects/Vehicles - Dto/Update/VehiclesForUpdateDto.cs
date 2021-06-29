using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Vehicles___Dto.Update
{
    public class VehiclesForUpdateDto
    {
        public string VehicleUtility { get; set; }

        public string VehicleDescription { get; set; }

        public Boolean VehicleAvailability { get; set; }

        public int FK_EstateID { get; set; }

        public int FK_EmployeeID { get; set; }
    }
}
