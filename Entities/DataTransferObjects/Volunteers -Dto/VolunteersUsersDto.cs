using Back_End.Entities;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Users___Dto.Users___Persons;
using Entities.DataTransferObjects.Locations___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class VolunteersUsersDto
    {
        public string UserDni { get; set; }
        public string RoleName { get; set; }
        public PersonsDto Persons { get; set; }
        public EstatesDto Estates { get; set; }
        public LocationsDto Locations { get; set; }
    }
}
