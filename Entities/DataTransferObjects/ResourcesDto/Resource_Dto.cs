using Back_End.Models.Employees___Dto;
using Back_End.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Materials___Dto;
using Entities.DataTransferObjects.Medicines___Dto;
using Entities.DataTransferObjects.Volunteers__Dto;

namespace Entities.DataTransferObjects.ResourcesDto
{
    public class Resource_Dto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public bool Availability { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }

        public string LocationCityName { get; set; }

        public MedicinesDto Medicines { get; set; }

        public MaterialsDto Materials { get; set; }

        public VehiclesDto Vehicles { get; set; }

        public VolunteerDto Volunteers { get; set; }

        public EstatesDto Estates { get; set; }
    }
}
