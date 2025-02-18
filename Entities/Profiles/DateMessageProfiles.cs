﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Helpers;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
    public class DateMessageProfiles: Profile
    {
        public DateMessageProfiles()
        {
            CreateMap<DateMessage, DateMessageDto>()
            ;

            CreateMap<DateMessage, DateMessageForCreationDto>();
            CreateMap<DateMessageForCreationDto, DateMessage>();

        }

    }
}
