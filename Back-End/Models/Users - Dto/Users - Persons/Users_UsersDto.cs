using Back_End.Models.Employees___Dto;
using System;


namespace Back_End.Models.Users___Dto.Users___Persons
{
    public class Users_UsersDto
    {
        public int UserID { get; set; }
        public string UserDni { get; set; }
        public Boolean UserAvailable { get; set; }
        public RolesDto Roles { get; set; }
        public Users_PersonsDto Persons { get; set; }
        public EstatesDto Estates { get; set; }
    }
}
