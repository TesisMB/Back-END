using Entities.DataTransferObjects.Resources_Request___Dto;

namespace Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto
{
    public class Resources_RequestResources_Materials_Medicines_VehiclesForCreationDto
    {
        public Resources_MaterialsForCreationDto Resources_Materials { get; set; }
        public Resources_MedicinesForCreationDto Resources_Medicines { get; set; }
        public int? FK_VehiclesID { get; set; }
    }
}
