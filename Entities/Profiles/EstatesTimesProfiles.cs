using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;

namespace Back_End.Profiles
{
    public class EstatesTimesProfiles: Profile
    {
        public EstatesTimesProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<EstatesTimes, EstatesTimesDto>();
        }
    }
}
