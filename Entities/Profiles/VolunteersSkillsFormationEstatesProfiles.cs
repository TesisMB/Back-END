using AutoMapper;
using Entities.DataTransferObjects.VolunteersSkillsFormationEstates;
using Entities.Helpers;
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
            CreateMap<VolunteersSkillsFormationEstates, VolunteersSkillsFormationEstatesDto>()
                .ForPath(dest => dest.FormationEstateName,
                            opt => opt.MapFrom(src => src.FormationsEstates.FormationEstateName))
            .ForPath(dest => dest.Date,
                            opt => opt.MapFrom(src => DateTimeOffsetExtensions.GetDate(src.FormationsEstates.FormationsDates.Date)))
                 
            .ForPath(dest => dest.FormationDateID,
                            opt => opt.MapFrom(src => src.FormationsEstates.ID))

            .ForPath(dest => dest.FormationEstateID,
                            opt => opt.MapFrom(src => src.FormationsEstates.ID));

            CreateMap<VolunteersSkillsFormationEstatesForCreationDto, VolunteersSkillsFormationEstates>();
        }
    }
}
