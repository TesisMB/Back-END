using Back_End.Models.Employees___Dto;
<<<<<<< Updated upstream
using Entities.DataTransferObjects.Locations___Dto;
=======
using Entities.DataTransferObjects.Employees___Dto;
using Entities.DataTransferObjects.Estates___Dto;
>>>>>>> Stashed changes
using System;


namespace Back_End.Models.Users___Dto.Users___Persons
{
    public class EmployeesUsersDto
    {
        public int UserID { get; set; }
<<<<<<< Updated upstream
=======
        public Boolean UserAvailability { get; set; }

>>>>>>> Stashed changes
        public string UserDni { get; set; }
        public Boolean UserAvailability { get; set; }
        public string RoleName { get; set; }
        public EmployeesPersonsDto Persons { get; set; }
<<<<<<< Updated upstream
        public EstatesDto Estates { get; set; }
        public LocationsDto Locations { get; set; }

=======

        public EstateDto Estates { get; set; }
>>>>>>> Stashed changes
    }
}
