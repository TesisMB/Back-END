using Back_End.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Resources_Request", Schema="dbo")]
    public class Resources_Request
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public Boolean Status { get; set; }

        [ForeignKey("FK_UserID")]
        public Users Users { get; set; }

        [ForeignKey("FK_EmergencyDisasterID")]
        public EmergenciesDisasters EmergenciesDisasters { get; set; }

        [Required]
        public int FK_UserID { get; set; }

        [Required]
        public int FK_EmergencyDisasterID { get; set; }

        public Resources  Resources { get; set; }
    }
}
