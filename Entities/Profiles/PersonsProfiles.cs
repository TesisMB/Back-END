using AutoMapper;
using Back_End.Models;
using Back_End.Models.Users___Dto.Users___Persons;
using Entities.DataTransferObjects.Employees___Dto;
using Entities.DataTransferObjects.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Volunteers__Dto;

namespace Back_End.Profiles
{
    public class PersonsProfiles : Profile
    {
        public PersonsProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Persons, PersonsDto>();
            CreateMap<Persons, EmployeePersonDto>();

            CreateMap<Persons, PersonsAppDto>();

            CreateMap<Persons, EmployeesPersonsDto>();

            CreateMap<Persons, PersonsVehiclesDto>();


            CreateMap<PersonForCreationDto, Persons>();

            CreateMap<PersonsForUpdatoDto, Persons>();
            CreateMap<Persons, PersonsForUpdatoDto>();


            CreateMap<UserEmployeeAuthDto, Persons>();

        }
    }
}
