using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models
{
    [Table("EstatesTimes", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql

    public class EstatesTimes

    {
        [Required]
        public int FK_EstateID { get; set; }

        public Estates Estates { get; set; }

        [Required]
        public int FK_TimeID { get; set; }

        public Times Times { get; set; }
    }
}
