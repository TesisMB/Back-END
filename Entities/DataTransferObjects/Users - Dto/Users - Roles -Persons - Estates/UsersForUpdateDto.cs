using System;


namespace Back_End.Models
{
    public class UsersForUpdateDto
    {
        public string UserPassword { get; set; }
        public string UserNewPassword { get; set; }
        public Boolean UserAvailability { get; set; }
        public int FK_RoleID { get; set; }
        public int FK_EstateID { get; set; }
        public PersonsForUpdatoDto Persons { get; set; }
    }
}
