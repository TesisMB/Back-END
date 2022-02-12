using Back_End.Models;
using System.ComponentModel.DataAnnotations.Schema;

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
