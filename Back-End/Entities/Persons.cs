using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{
    [Table("Persons", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Persons
    {
        //Defino tanto como clave primaria, como forranea ID, para poder efecturar la relacion 1-1 con Users
        [Key, ForeignKey("Users")]
        public int ID { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(12)]
        public string Phone { get; set; }
        
        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(1)]
        public string Gender { get; set; }

        [MaxLength(7)]
        public DateTimeOffset Birthdate { get; set; }

        [Required]
        public Boolean Available { get; set; }
        public Users Users { get; set; }
    }
}
