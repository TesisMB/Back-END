using Back_End.Models.Users_Dto;
using System;


namespace Back_End.Models.Users___Dto
{
    public class UsersVolunteersForCreationDto : UserDniMustBeDifferentFromUserDniDto
    {
        [UserDniMustBeDifferentFromUserDniDto(
        ErrorMessage = "El Dni que ingreso ya existe")]
        public string UserDni { get; set; }
        public string UserPassword { get; set; }
        public Boolean UserAvailability { get; set; }
        public int FK_RoleID { get; set; }
        public PersonForCreationDto Persons { get; set; }
    }
}
