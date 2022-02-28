using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    public class VehiclesResourcesDto
    {
        public int ResourceID { get; set; }

        public string Name { get; set; }

        public string VehiclePatent { get; set; }

        public string Type { get; set; }

        public int VehicleYear { get; set; }
    }
}
