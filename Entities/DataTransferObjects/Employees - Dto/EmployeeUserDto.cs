using Back_End.Models.Employees___Dto;
using System;

namespace Entities.DataTransferObjects.Employees___Dto
{
    public class EmployeeUserDto
    {
        public int UserID { get; set; }
        public Boolean UserAvailability { get; set; }

        public string UserDni { get; set; }

        public string Createdate { get; set; }
        public string Avatar { get; set; }

        public int FK_EstateID { get; set; }
        public int FK_RoleID { get; set; }

        public string RoleName { get; set; }
        public EmployeePersonDto Persons { get; set; }

        public EstatesDto Estates { get; set; }
    }
}
