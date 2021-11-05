using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Resources", Schema="dbo")]
    public class Resources
    {
        [Key, ForeignKey("Resources_Request")]
        public int ID { get; set; }

        [ForeignKey("FK_Resource_MaterialID")]
        public Resources_Materials Resources_Materials { get; set; }

        [ForeignKey("FK_Resource_MedicinelID")]
        public Resources_Medicines Resources_Medicines { get; set; }

        [ForeignKey("FK_Resource_VehiclelID")]
        public Resources_Vehicles Resources_Vehicles { get; set; }

        public int? FK_Resource_MaterialID { get; set; }

        public int? FK_Resource_MedicinelID { get; set; }

        public int? FK_Resource_VehiclelID { get; set; }

        public Resources_Request Resources_Request { get; set; }
    }
}
