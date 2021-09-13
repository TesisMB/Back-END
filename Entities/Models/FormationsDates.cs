using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("FormationsDates", Schema = "dbo")]
   public class FormationsDates
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ICollection<FormationsEstates> FormationsEstates { get; set; }
    }
}
