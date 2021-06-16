using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{
    [Table("EstatesTimes", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql

    public class EstatesTimes

    {   [Required]
        public int FK_EstateID { get; set; }

        public Estates Estates { get; set; }

       [Required]
        public int FK_TimeID { get; set; }

        public Times Times { get; set; }
    }
}
