using System;

namespace Back_End.Models
{
    public class UsersForUpdateDto
    {
        public int userID { get; set; }
        public string UserDni { get; set; }
        public string UserPassword { get; set; }
        public string UserNewPassword { get; set; }
        public Boolean UserAvailability { get; set; }
        public int FK_RoleID { get; set; }
        public int FK_EstateID { get; set; }
        public int FK_LocationID { get; set; }
        public string avatar { get; set; }
        public PersonsForUpdatoDto Persons { get; set; }
    }
}
