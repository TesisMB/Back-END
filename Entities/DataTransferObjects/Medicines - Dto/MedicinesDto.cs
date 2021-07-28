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
        public String MedicineQuantity { get; set; }
        public DateTimeOffset MedicineExpirationDate { get; set; }
        public String MedicineLab { get; set; }
        public String MedicineDrug { get; set; }
        public String MedicineWeight { get; set; }
        public String MedicineUtility { get; set; }
        public Boolean MedicineAvailability { get; set; }
        public EstatesDto Estates { get; set; }
    }
}
