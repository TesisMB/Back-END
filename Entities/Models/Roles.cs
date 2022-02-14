using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models
{
    [Table("Roles", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; }

        public ICollection<Users> Users { get; set; }

    }
}
