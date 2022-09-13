using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class DateMessageForCreationDto
    {
        public int FK_ChatRoomID { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string CreatedDate { get; set; }

    }
}
