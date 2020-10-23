using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Entities
{
    [Table("Estate", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Estate
    {

            [Key]
            public int EstateID { get; set; }

            [Required]
            [MaxLength(50)]
            public string EstateAddress { get; set; }

            [Required]
            public int EstateNumber { get; set; }

            [Required]
            [MaxLength(100)]
            public string EstateResponsible { get; set; }

            [Required]
            [MaxLength(16)]
            public string EstatePhone { get; set; }

            [Required]
            [MaxLength(50)]
            public string EstateCity { get; set; }

            [Required]
            [MaxLength(4)]
            public string EstatePostalCode { get; set; }
    }
    }

