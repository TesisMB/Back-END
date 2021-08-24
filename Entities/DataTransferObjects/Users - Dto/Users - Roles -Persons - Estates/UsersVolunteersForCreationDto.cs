using System;


namespace Back_End.Models.Users___Dto
{
    public class UsersVolunteersForCreationDto //: UserDniMustBeDifferentFromUserDniDto
    {
        //[UserDniMustBeDifferentFromUserDniDto(
        //ErrorMessage = "El Dni que ingreso ya existe")]
        public string UserDni { get; set; }
        public string UserPassword { get; set; }
        public Boolean UserAvailability { get; set; } = true;
        public int FK_RoleID { get; set; }
        public int FK_EstateID { get; set; }
        public int FK_LocationID { get; set; }

        public PersonForCreationDto Persons { get; set; }
    }
}
