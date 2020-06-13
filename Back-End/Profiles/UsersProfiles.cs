using AutoMapper;
using Back_End.Helpers;

namespace Back_End.Profiles
{
    public class UsersProfile : Profile
    {

        public UsersProfile()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Models.Users, Dto.UsersDto>()


                    //Creo dos variables nuevas no existentes en la Base de datos

                    //Mapeo entre Name y LastName de Users para devolver FullName (uniendo ambos valores)
                    .ForMember(
                        dest => dest.FullName,
                        opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"))

                       .ForMember(dest => dest.RoleName,
                                    opt => opt.MapFrom(src => src.Roles.RoleName))


                    //Mapeo la Fecha de Nacimiento para devolver solamente la edad
                    .ForMember(
                        dest => dest.Age,
                        opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()));


            CreateMap<Dto.UsersForCreationDto, Models.Users>();
            CreateMap<Dto.UsersForUpdate, Models.Users>();
            CreateMap<Models.Users, Dto.UsersForUpdate>();


        }


    };
}

