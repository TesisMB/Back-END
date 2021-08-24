using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("Alerts", Schema="dbo")]
   public class Alerts
    {
        [Key]
        [Column("ID")]
        public int AlertID { get; set; }

        [Required]
        public string AlertMessage { get; set; }

        [Required]
        [MaxLength(50)]
        public string AlertDegree { get; set; }
        public ICollection<EmergenciesDisasters> EmergenciesDisasters { get; set; }

    }
}
