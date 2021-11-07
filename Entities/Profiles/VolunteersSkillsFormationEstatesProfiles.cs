using AutoMapper;
using Entities.DataTransferObjects.VolunteersSkillsFormationEstates;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
   public class VolunteersSkillsFormationEstatesProfiles: Profile
    {
        public VolunteersSkillsFormationEstatesProfiles()
        {
            CreateMap<VolunteersSkillsFormationEstates, VolunteersSkillsFormationEstatesDto>();
        }
    }
}
