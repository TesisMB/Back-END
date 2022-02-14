

namespace Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto
{
    public class ResourcesRequestMaterialsMedicinesVehiclesDto
    {
        public int ID { get; set; }
        public int? FK_MaterialID { get; set; }
        public int? FK_MedicineID { get; set; }
        public int? FK_VehicleID { get; set; }
        public int Quantity { get; set; }
    }
}
