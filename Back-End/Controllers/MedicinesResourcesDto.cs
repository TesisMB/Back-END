using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    public class MedicinesResourcesDto
    {
        public int ResourceID { get; set; }

        public string Name { get; set; }

        public string MedicineExpirationDate { get; set; }

        public string MedicineLab { get; set; }

        public string MedicineDrug { get; set; }

        public double MedicineWeight { get; set; }

        public string MedicineUnits { get; set; }
    }
}
