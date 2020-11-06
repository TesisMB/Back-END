using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class MaterialsDto
    {
        public int MaterialD { get; set; }

        public string MaterialName { get; set; }

        public int MaterialQuantity { get; set; }

        public string MaterialLocation { get; set; }

        public string MaterialUtility { get; set; }

        public bool MaterialAvailability { get; set; }

        public Estate Estate { get; set; }

        public int EstateID { get; set; }

        public string MaterialsUtility { get; set; }
    }
}
