using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{
    [Table("Employees", Schema = "dbo")]
    public class Employees
    {
        [Key, ForeignKey("Users")]
        public int ID { get; set; }

        [Required]
        public DateTimeOffset EmployeeCreatedate { get; set; }

        public Users Users { get; set; }
    }
}
