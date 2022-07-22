using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class DateDto
    {
        public int ID { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<DateMessageDto> DateMessage { get; set; }
}
}
