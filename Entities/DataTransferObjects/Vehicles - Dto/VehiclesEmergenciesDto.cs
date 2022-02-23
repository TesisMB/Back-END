using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Vehicles___Dto
{
    public class VehiclesEmergenciesDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string VehiclePatent { get; set; }

        public string Type { get; set; }

        public int VehicleYear { get; set; }
    }
}
