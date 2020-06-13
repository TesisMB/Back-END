using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    [Table("Users", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Users
    {

        [Key]
        public int IdUser { get; set; }

        // [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        //[Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(8)]
        public string Dni { get; set; }

        [Required]
        [MaxLength(16)]
        public string Password { get; set; }

        //[Required]
        [MaxLength(12)]
        public string Phone { get; set; }

        //[Required]
        [MaxLength(75)]
        public string Email { get; set; }

        //[Required]
        [MaxLength(1)]
        public string Gender { get; set; }

        //[Required]
        [MaxLength(8)]
        public string Address { get; set; }

        //   [Required]
        public DateTimeOffset DateOfBirth { get; set; }

        // [Required]
        public DateTimeOffset DateOfCreate { get; set; }

        [ForeignKey("IdRole")]
        public Roles Roles { get; set; }

        public int IdRole { get; set; }




    }
}
