﻿using System;

namespace Entities.DataTransferObjects.Messages___Dto
{
    public class MessagesDto
    {
        public int ID { get; set; }

        public String Message { get; set; }

        public string Avatar { get; set; }
        public Boolean MessageState { get; set; }

        public DateTime CreatedDate { get; set; }

        public int userID { get; set; }

        public string  RoleName { get; set; }
        public string Name { get; set; }

    }
}
