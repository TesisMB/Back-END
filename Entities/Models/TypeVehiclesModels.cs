using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("TypeVehiclesModels", Schema = "dbo")]
    public class TypeVehiclesModels
    {
        public int FK_TypeVehicleID { get; set; }

        public TypeVehicles TypeVehicles { get; set; }

        public int FK_ModelID { get; set; }

        public Model Models { get; set; }
    }
}
