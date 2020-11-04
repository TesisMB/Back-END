using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class MaterialsForCreation_UpdateDto
    {
        public int MaterialsQuantity { get; set; }

        public string MaterialsName { get; set; }

        public bool MaterialsIsAvailable { get; set; }

        public string MaterialsUtility { get; set; }
    }
}
