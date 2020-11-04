using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class VehiclesDto
    {
        public int VehicleID { get; set; }

        public int VehicleQuantity { get; set; }

        public string VehicleBrand { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleName { get; set; }

        public string VehicleKms { get; set; }

        public bool VehicleIsAvailable { get; set; }

        public string VehicleUtility { get; set; }

        public string VehicleResponsible { get; set; }
    }
}
