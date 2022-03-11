using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models
{
    [Table("Persons", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Persons
    {
        //Defino tanto como clave primaria, como forranea ID, para poder efecturar la relacion 1-1 con Users
        [Key, ForeignKey("Users")]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(12)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        [MaxLength(1)]
        public string Gender { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Column("Available")]
        public Boolean Status { get; set; }

        [Required]
        public string LocationName { get; set; }

        public Users Users { get; set; }
    }
}
