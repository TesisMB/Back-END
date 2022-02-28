using System;

namespace Entities.DataTransferObjects.ResourcesRequestMaterialsMedicinesVehicles___Dto
{
    public class ResourcesMedicnesDto
    {
        public int ID { get; set; }
        public int Quantity { get; set; }

        public string Name { get; set; }

        public String MedicineExpirationDate { get; set; }

        public string MedicineLab { get; set; }

        public string MedicineDrug { get; set; }

        public double MedicineWeight { get; set; }

        public string MedicineUnits { get; set; }



    }
}
