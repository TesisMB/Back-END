using AutoMapper;
using Entities.DataTransferObjects.Messages___Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
    public class MessagesProfiles: Profile
    {
        public MessagesProfiles()
        {
            CreateMap<Messages, MessagesForCreationDto>();
            CreateMap<MessagesForCreationDto, Messages>();
        }
    }
}
