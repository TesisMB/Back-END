using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    [Table("Estates", Schema = "dbo")]
    public class Estates
    {
        [Column("ID")]
        [Key]
        public int EstateID { get; set; }

        [Required]
        [MaxLength(16)]
        public string EstatePhone { get; set; }

        [Required]
        [MaxLength(50)]
        public string EstateTypes { get; set; }

        public Boolean EstateAvailability { get; set; }


        [ForeignKey("FK_EstateID")]
        public ICollection<EstatesTimes> EstatesTimes { get; set; }

        public LocationAddress LocationAddress { get; set; }

        public ICollection<Users> Users { get; set; }

        public ICollection<Vehicles> Vehicles { get; set; }

        public ICollection<Medicines> Medicines { get; set; }

    }
}
