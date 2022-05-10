using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.Resources_Request___Dto
{
    public class ResourcesRequestForCreationDto
    {
        public DateTime RequestDate { get; set; } = DateTime.Now;

        public string? Reason { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; } = false;

        public string Condition { get; set; } = "Pendiente";

        public int FK_UserID { get; set; }

        public int? ModifiedBy { get; set; }

        public int FK_EmergencyDisasterID { get; set; }

        public  int UserRequest { get; set; }
        public ICollection<ResourcesRequestMaterialsMedicinesVehiclesForCreationDto> Resources_RequestResources_Materials_Medicines_Vehicles { get; set; }

    }
}
