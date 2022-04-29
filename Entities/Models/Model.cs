using Back_End.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Model", Schema = "dbo")]
    public class Model
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ModelName { get; set; }

        public ICollection<Vehicles> Vehicles { get; set; }

    }
}
