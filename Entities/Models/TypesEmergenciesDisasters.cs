using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("TypesEmergenciesDisasters", Schema="dbo")]
    public class TypesEmergenciesDisasters
    {
        [Key]
        [Column("ID")]
        public int TypeEmergencyDisasterID { get; set; }

        [Required]
        [MaxLength(50)]
        public string TypeEmergencyDisasterName { get; set; }

        public string TypeEmergencyDisasterIcon { get; set; }

        public string TypeEmergencyDisasterDescription { get; set; }

        public ICollection<EmergenciesDisasters> EmergenciesDisasters { get; set; }


    }
}
