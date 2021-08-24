using Back_End.Models.Employees___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Medicines___Dto
{
   public class MedicinesDto
    {
        public int MedicineID { get; set; }

        public String MedicineName { get; set; }

        public int MedicineQuantity { get; set; }
        public DateTime MedicineExpirationDate { get; set; }

        public String MedicineLab { get; set; }

        public String MedicineDrug { get; set; }

        public double MedicineWeight { get; set; }

        public string MedicineUnits { get; set; }

        public String MedicineUtility { get; set; }

        public Boolean MedicineAvailability { get; set; }
        public EstatesDto Estates { get; set; }
    }
}
