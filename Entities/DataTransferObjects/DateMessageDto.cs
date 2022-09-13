using Entities.DataTransferObjects.Messages___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class DateMessageDto
    {
        public string CreatedDate { get; set; }

        public ICollection<MessagesDto> Messages { get; set; }

    }
}
