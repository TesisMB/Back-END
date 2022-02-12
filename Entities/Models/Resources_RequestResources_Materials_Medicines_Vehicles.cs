using Back_End.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Resources_RequestResources_Materials_Medicines_Vehicles", Schema = "dbo")]
    public class Resources_RequestResources_Materials_Medicines_Vehicles
    {
        [Key]
        public int ID { get; set; }

        public Resources_Request Resources_Request { get; set; }
        public int FK_Resource_RequestID { get; set; }

        public Materials Materials { get; set; }
        public string? FK_MaterialID { get; set; }

        public Medicines Medicines { get; set; }
        public string? FK_MedicineID { get; set; }

        public Vehicles Vehicles { get; set; }
        public string? FK_VehicleID { get; set; }

        public int Quantity { get; set; }
    }
}
