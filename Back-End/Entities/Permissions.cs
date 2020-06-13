using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SICREYD.Entities
{
    [Table("Permissions", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql

    public class Permissions
    {

        [Required()]
        [Key()]
        public int IdPermission { get; set; }

        [Required()]
        public string PermissionType { get; set; }

        [Required()]
        public string PermissionValue { get; set; } 

         [ForeignKey("IdRole")]
        public Roles roles { get; set; }
        public int IdRole { get; set; }


    }
}
