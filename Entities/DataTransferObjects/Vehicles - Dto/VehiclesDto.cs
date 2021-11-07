using Back_End.Models.Employees___Dto;
using Back_End.Models.TypeVehicles___Dto;
using Entities.DataTransferObjects.MarksModels___Dto;
using Entities.DataTransferObjects.Models.Vehicles___Dto;
using System;


namespace Back_End.Models.Vehicles___Dto
{
    public class VehiclesDto
    {
        public string VehiclePatent { get; set; }

        public string Utility { get; set; }

        public EmployeesVehiclesDto Employees { get; set; }
        public TypeVehiclesDto Type { get; set; }

        public BrandsModelsDto BrandsModels { get; set; }

    }
}
