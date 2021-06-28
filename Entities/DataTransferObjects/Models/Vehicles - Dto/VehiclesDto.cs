using Back_End.Models.Employees___Dto;
using Back_End.Models.TypeVehicles___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Vehicles___Dto
{
    public class VehiclesDto
    {
        public int VehicleID { get; set; }

        public string VehiclePatent { get; set; }

        public string VehicleUtility { get; set; }

        public string VehicleDescription { get; set; }

        public Boolean VehicleAvailability { get; set; }

        public EstatesDto Estates { get; set; }

        public EmployeesDto Employees { get; set; }

        public TypeVehiclesDto TypeVehicles { get; set; }
    }
}
