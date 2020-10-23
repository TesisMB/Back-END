using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.MaterialsDto
{
    public class MaterialsDto
    {
        public int MaterialsID { get; set; }

        public int MaterialsQuantity { get; set; }
      
        public string MaterialsName { get; set; }

        public bool MaterialsIsAvailable { get; set; }

        public Estate Estate { get; set; }

        public int EstateID { get; set; }

        public string MaterialsUtility { get; set; }
    }
}
