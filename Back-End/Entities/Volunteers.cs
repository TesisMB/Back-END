using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{
    [Table ("Volunteers", Schema = "dbo")]
    public class Volunteers
    {
        [Key, ForeignKey("Users")]
        public int ID { get; set; }

        public string VolunteerAvatar { get; set; }

        public string VolunteerDescription { get; set; }

    }
}
