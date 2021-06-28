using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    [Table("Estates", Schema ="dbo")]
    public class Estates
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(16)]
        public string EstatePhone { get; set; }

        [Required]
        [MaxLength(50)]
        public string EstateTypes { get; set; }

        public Boolean EstateAvailability { get; set; }
      
        public LocationAddress LocationAddress { get; set; }


        public ICollection<Users> Users { get; set; }

        [ForeignKey("FK_EstateID")]
        public ICollection<EstatesTimes> EstatesTimes { get; set; }

    }
}
