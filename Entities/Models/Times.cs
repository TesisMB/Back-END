using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models
{
    [Table("Times", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql

    public class Times
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [ForeignKey("FK_ScheduleID")]
        public Schedules Schedules { get; set; }

        [Required]
        public int FK_ScheduleID { get; set; }

        [ForeignKey("FK_TimeID")]

        public ICollection<EstatesTimes> EstatesTimes { get; set; }
    }
}
