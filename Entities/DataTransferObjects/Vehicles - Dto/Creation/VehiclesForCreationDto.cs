using Entities.DataTransferObjects.Brands___Dto;
using Entities.DataTransferObjects.TypeVehicles___Dto;
using Microsoft.AspNetCore.Http;
using System;

namespace Entities.DataTransferObjects.Vehicles___Dto.Creation
{
    public class VehiclesForCreationDto
    {
        public string VehiclePatent { get; set; }

        public string VehicleUtility { get; set; }

        public int VehicleYear { get; set; }

        public string VehicleDescription { get; set; }

        public Boolean VehicleAvailability { get; set; } = true;
        public IFormFile ImageFile { get; set; }

        public int? FK_EstateID { get; set; }
        public int? FK_EmployeeID { get; set; }
        public int Fk_TypeVehicleID { get; set; }

        public TypeVehiclesForCreationDto TypeVehicles { get; set; }
        public BrandsModelsForCreationDto BrandsModels { get; set; }

    }
}
