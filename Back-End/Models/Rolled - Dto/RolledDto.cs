using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.RolledDto
{
    public class RolledDto
    {
        public int RolledID { get; set; }

        public int RolledQuantity { get; set; }

        public string RolledBrand { get; set; }

        public string RolledModel { get; set; }

        public string RolledName { get; set; }

        public string RolledKms { get; set; }

        public bool RolledIsAvailable { get; set; }

        public string RolledUtility { get; set; }

        public string RolledResponsible { get; set; }
    }
}
