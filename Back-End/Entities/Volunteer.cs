using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Entities
{
    [Table("Volunteer", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Volunteer
    {
        [Key]
        public int VolunteerID { get; set; }

        [Required]
        [MaxLength(100)]
        public string VolunteerFirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string VolunteerLastName { get; set; }

        [Required]
        [MaxLength(8)]
        public string VolunteerDni { get; set; }

        [Required]
        [MaxLength(50)]
        public string VolunteerAddress { get; set; }

        [Required]
        [MaxLength(12)]
        public string VolunteerPhone { get; set; }

        [Required]
        [MaxLength(50)]
        public string VolunteerEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string VolunteerSpecialty { get; set; }

        [Required]
        public string VolunteerAvatar { get; set; }
    }
}
