using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.Materials___Dto;
using Entities.DataTransferObjects.Medicines___Dto;
using Entities.DataTransferObjects.Vehicles___Dto.Creation;
using Microsoft.AspNetCore.Http;
using System;

namespace Entities.DataTransferObjects.ResourcesDto
{
    public class Resources_ForCreationDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Boolean Availability { get; set; }
        public string Picture { get; set; }

        public IFormFile ImageFile { get; set; }

        public string Description { get; set; }

        public int FK_EstateID { get; set; }

        public Boolean Donation { get; set; }
        public MaterialsDto Materials { get; set; }

        public MedicineForCreationDto Medicines { get; set; }

        public VehiclesForCreationDto  Vehicles { get; set; }

    }
}
