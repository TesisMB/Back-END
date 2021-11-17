using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.Locations___Dto;
using Entities.DataTransferObjects.Employees___Dto;
using Entities.DataTransferObjects.Estates___Dto;
using System;


namespace Back_End.Models.Users___Dto.Users___Persons
{
    public class EmployeesUsersDto
    {
        public int UserID { get; set; }
        public Boolean UserAvailability { get; set; }

        public string UserDni { get; set; }
        public string RoleName { get; set; }
        public EmployeesPersonsDto Persons { get; set; }
        public EstatesDto Estates { get; set; }
        public LocationsDto Locations { get; set; }


    }
}
