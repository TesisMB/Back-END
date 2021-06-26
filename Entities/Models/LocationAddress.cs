using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    [Table("LocationAddress", Schema="dbo")]
    public class LocationAddress
    {
        [Key, ForeignKey("Estates")]
        public int ID { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }


        [MaxLength(50)]
        public string NumberAddress { get; set; }

        public string DescriptionAddress { get; set; }

        public Estates Estates { get; set; }
    }
}
