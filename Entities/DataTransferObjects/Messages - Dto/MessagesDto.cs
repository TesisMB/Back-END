using System;

namespace Entities.DataTransferObjects.Messages___Dto
{
    public class MessagesDto
    {
        public int ID { get; set; }

        public String Message { get; set; }

        public Boolean MessageState { get; set; }

        public DateTime CreatedDate { get; set; }

        public int FK_UserID { get; set; }
        
    }
}
