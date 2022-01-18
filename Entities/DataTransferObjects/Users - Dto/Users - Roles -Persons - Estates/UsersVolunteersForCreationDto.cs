using System;


namespace Back_End.Models.Users___Dto
{
    public class UsersVolunteersForCreationDto 
    {
        public string UserDni { get; set; }
        public string UserPassword { get; set; }
        public Boolean UserAvailability { get; set; }
        public int FK_RoleID { get; set; }
        public int FK_EstateID { get; set; }
        public PersonForCreationDto Persons { get; set; }
    }
}
