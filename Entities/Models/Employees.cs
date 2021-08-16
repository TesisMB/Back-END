using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    [Table("Employees", Schema = "dbo")]
    public class Employees
    {
        [Key, ForeignKey("Users")]
        [Column("ID")]
        public int EmployeeID { get; set; }

        [Required]
        public DateTime EmployeeCreatedate { get; set; }

        public Users Users { get; set; }

        public ICollection<Vehicles> Vehicles { get; set; }
    }
}
