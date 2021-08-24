using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("EmergenciesDisasters", Schema = "dbo")]
    public class EmergenciesDisasters
    {
        [Key]
        [Column("ID")]
        public int EmergencyDisasterID { get; set; }

        [Required]
        public DateTime EmergencyDisasterStartDate { get; set; }

        public DateTime? EmergencyDisasterEndDate { get; set; }

        public string EmergencyDisasterInstruction { get; set; }


        [ForeignKey("Fk_EmplooyeeID")]
        public Employees Employees { get; set; }

        [ForeignKey("FK_LocationID")]
        public Locations Locations { get; set; }

        [ForeignKey("FK_TypeEmergencyID")]
        public TypesEmergenciesDisasters TypesEmergenciesDisasters { get; set; }

        [ForeignKey("FK_AlertID")]
        public Alerts Alerts { get; set; }


        public int? Fk_EmplooyeeID { get; set; }

        [Required]
        public int FK_LocationID { get; set; }

        [Required]
        public int FK_TypeEmergencyID { get; set; }

        [Required]
        public int FK_AlertID { get; set; }

    }

}
