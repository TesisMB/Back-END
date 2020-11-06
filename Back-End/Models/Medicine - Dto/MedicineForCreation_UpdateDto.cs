using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class MedicineForCreation_UpdateDto
    {
        public string MedicineName { get; set; }

        public string MedicineWeight { get; set; }

        public string MedicineLab { get; set; }

        public string MedicineDrug { get; set; }
    }
}
