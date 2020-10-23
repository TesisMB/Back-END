using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.MedicineDto
{
    public class MedicineDto
    {
        public int MedicineID { get; set; }

        public string EstateName { get; set; }

        public DateTime EstateDateExpire { get; set; }

        public Estate Estate { get; set; }
        public int EstateID { get; set; }
    }
}
