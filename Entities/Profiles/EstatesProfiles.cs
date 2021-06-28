using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;

namespace Back_End.Profiles
{
    public class EstatesProfiles : Profile
    {
       public EstatesProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Estates, EstatesDto>();

        }
    }
}
