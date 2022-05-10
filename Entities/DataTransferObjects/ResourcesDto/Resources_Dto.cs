using Back_End.Models.Employees___Dto;
using Back_End.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Materials___Dto;
using Entities.DataTransferObjects.Medicines___Dto;
using Microsoft.AspNetCore.Http;
using System;

namespace Entities.DataTransferObjects.ResourcesDto
{
    public class Resources_Dto
    {
        public String ID { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public Boolean Availability { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string ImageSrc { get; set; }
        public Boolean Donation { get; set; }

        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public string CreatedByEmployee { get; set; }
        public string ModifiedByEmployee { get; set; }

        public string LocationCityName { get; set; }

        public MedicinesDto Medicines { get; set; }

        public MaterialsDto Materials { get; set; }

        public VehiclesDto Vehicles { get; set; }

        public VolunteersDto Volunteers { get; set; }

        public EstatesDto Estates { get; set; }
    }
}
