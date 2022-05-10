using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Employees EmployeeIncharge { get; set; }


        [ForeignKey("ModifiedBy")]
        public Employees EmployeeModified { get; set; }

        [ForeignKey("CreatedBy")]
        public Employees EmployeeCreated { get; set; }

        public LocationsEmergenciesDisasters LocationsEmergenciesDisasters { get; set; }

        [ForeignKey("FK_TypeEmergencyID")]
        public TypesEmergenciesDisasters TypesEmergenciesDisasters { get; set; }

        [ForeignKey("FK_AlertID")]
        public Alerts Alerts { get; set; }

        [ForeignKey("FK_EstateID")]
        public Estates Estates { get; set; }

        public ChatRooms ChatRooms { get; set; }

        public Victims Victims { get; set; }

        public DateTime? EmergencyDisasterDateModified { get; set; }

        [Required]
        public int? Fk_EmplooyeeID { get; set; }

        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }


        [Required]
        public int FK_TypeEmergencyID { get; set; }

        [Required]
        public int FK_AlertID { get; set; }

        [Required]
        public int FK_EstateID { get; set; }

        [Required]
        public ICollection<ResourcesRequest> Resources_Requests { get; set; }

        [ForeignKey("FK_EmergencyDisasterID")]
        public ICollection<VolunteersLocationVolunteersEmergenciesDisasters> VolunteersLocationVolunteersEmergenciesDisasters { get; set; }

    }

}
