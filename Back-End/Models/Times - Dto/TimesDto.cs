using Back_End.Models.Persons___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Employees___Dto
{
    public class TimesDto
    {
        public int TimeID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public SchedulesDto Schedules { get; set; }
    }
}
