using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    [Table("Permissions", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql

    public class Permissions
    {

        [Required()]
        [Key()]
        public int IdPermission { get; set; }

        [Required()]
        [MaxLength(50)]
        public string PermissionType { get; set; }

        [Required()]
        [MaxLength(50)]
        public string PermissionValue { get; set; }

        [ForeignKey("IdRole")]
        public int IdRole { get; set; }


    }
}
