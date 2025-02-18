﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Materials___Dto
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
