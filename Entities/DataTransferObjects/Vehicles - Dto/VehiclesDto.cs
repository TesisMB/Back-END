using Back_End.Models.Employees___Dto;
using Back_End.Models.TypeVehicles___Dto;
using Entities.DataTransferObjects.Models.Vehicles___Dto;
using System;


namespace Back_End.Models.Vehicles___Dto
{
    public class VehiclesDto
    {
        public int VehicleID { get; set; }

        public string VehiclePatent { get; set; }

        public string VehicleUtility { get; set; }

        public string VehicleDescription { get; set; }

        public Boolean VehicleAvailability { get; set; }

        public string VehiclePicture { get; set; }

        public EstatesVehiclesDto Estates { get; set; }
        public EmployeesVehiclesDto Employees { get; set; }
        public TypeVehiclesDto TypeVehicles { get; set; }
    }
}
