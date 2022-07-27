using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class DateMessageForCreationDto
    {
        public int FK_ChatRoomID { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
