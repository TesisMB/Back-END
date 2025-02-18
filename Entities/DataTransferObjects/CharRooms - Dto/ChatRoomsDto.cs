﻿using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.DataTransferObjects.Messages___Dto;
using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.CharRooms___Dto
{
    public class ChatRoomsDto
    {
        public int ID { get; set; }

        //public DateTime CreationDate { get; set; }

        public EmergenciesDisastersDto EmergenciesDisasters { get; set; }

        public ICollection<DateMessageDto> DateMessage { get; set; }


        public ICollection<UsersChatRoomsDto> UsersChatRooms { get; set; }

        //public ICollection<MessagesDto> Messages { get; set; }

        public int Quantity { get; set; }

        public string LastMessage { get; set; }

    }
}
