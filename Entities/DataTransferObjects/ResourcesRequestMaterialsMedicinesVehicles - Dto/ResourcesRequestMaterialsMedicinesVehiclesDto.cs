
namespace Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto
{
    public class ResourcesRequestMaterialsMedicinesVehiclesDto
    {
        public int ID { get; set; }
        public int Quantity { get; set; }

        //NO
        public int ResourceID { get; set; }

        //NO
        public string Name { get; set; }

        public string MedicineExpirationDate { get; set; }

        public string MedicineLab { get; set; }

        public string MedicineDrug { get; set; }

        public double MedicineWeight { get; set; }

        public string MedicineUnits { get; set; }
        public string Brand { get; set; }
        


        public string VehiclePatent { get; set; }

        public string Type { get; set; }

        public int VehicleYear { get; set; }

    }
}

