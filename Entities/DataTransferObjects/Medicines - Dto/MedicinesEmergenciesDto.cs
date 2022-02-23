using System;

namespace Entities.DataTransferObjects.Medicines___Dto
{
    public class MedicinesEmergenciesDto
    {
        public int ID { get; set; }
        public string MedicineName { get; set; }
        public string MedicineExpirationDate { get; set; }

        public string MedicineLab { get; set; }

        public string MedicineDrug { get; set; }

        public double MedicineWeight { get; set; }

        public string MedicineUnits { get; set; }
    }
}
