
using Entities.DataTransferObjects.ResourcesRequestMaterialsMedicinesVehicles___Dto;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto
{
    public class ResourcesRequestMaterialsMedicinesVehiclesDto
    {
        public int ID { get; set; }

        public int FK_Resource_RequestID { get; set; }

        public ResourcesMaterialsDto Materials { get; set; }
        public ResourcesMedicnesDto Medicines { get; set; }

        public ResourcesVehiclesDto Vehicles { get; set; }

    }
}

