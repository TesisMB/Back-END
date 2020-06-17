using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{


        [Table("Users", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Users
    {

        [Key]
        public int UserID { get; set; }

       // [Required]
        [MaxLength(100)]
        public string UserFirstName { get; set; }

        //[Required]
        [MaxLength(100)]
        public string UserLastname { get; set; }

        [Required]
        [MaxLength(8)]
        public string UserDni { get; set; }

        [Required]
        [MaxLength(16)]
        public string UserPassword { get; set; }

        //[Required]
        [MaxLength(12)]
        public string UserPhone { get; set; }

        //[Required]
        [MaxLength(75)]
        public string UserEmail { get; set; }

        //[Required]
        [MaxLength(1)]
        public string UserGender { get; set; }

        //[Required]
   
        public string UserAddress{ get; set; }

     //   [Required]
        public DateTimeOffset UserBirthdate { get; set; }

       // [Required]
        public DateTimeOffset UserCreatedate { get; set; }

        public string UserAvatar { get; set; }

        [ForeignKey("IdRole")]
        public Roles RoleName { get; set; }
        public int IdRole { get; set; }




    }
}
