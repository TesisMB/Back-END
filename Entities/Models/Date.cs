using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class Date
    {
        [Key]
        public int ID { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<DateMessage> DateMessage { get; set; }
    }
}
