using Back_End.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Resources_Request___Dto;

namespace Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto
{
    public class Resources_RequestResources_Materials_Medicines_VehiclesDto
    {
        public int ID { get; set; }
        public Resources_MaterialsDto? Resources_Materials { get; set; }
        public Resources_MedicinesDto? Resources_Medicines { get; set; }
        public VehiclesDto? Vehicles { get; set; }
    }
}
