using Entities.DataTransferObjects.Materials___Dto;
using Entities.DataTransferObjects.Medicines___Dto;
using Entities.DataTransferObjects.Vehicles___Dto.Creation;
using System;

namespace Entities.DataTransferObjects.ResourcesDto
{
    public class Resources_ForCreationDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Boolean Availability { get; set; } = true;
        public string Picture { get; set; }

        public string? ImageFile { get; set; }

        public string Description { get; set; }

        public int FK_EstateID { get; set; }

        public Boolean Donation { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;


        public int CreatedBy { get; set; }

        public string Reason { get; set; }
        public bool Enabled { get; set; } = true;

        public MaterialsDto Materials { get; set; }

        public MedicineForCreationDto Medicines { get; set; }

        public VehiclesForCreationDto Vehicles { get; set; }

    }
}