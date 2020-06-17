using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{
    [Table("Permissions", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql

    public class Permissions
    {

        [Required()]
        [Key()]
        public int PermissionID { get; set; }

        [Required()]
        public string PermissionType { get; set; }

        [Required()]
        public string PermissionValue { get; set; } 

         [ForeignKey("RoleID")]
       // public string RoleName { get; set; }
        public int RoleID { get; set; }


    }
}
