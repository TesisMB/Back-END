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
        public DateTime? Birthdate { get; set; }
        public string Address { get; set; }
        public Boolean Status { get; set; }

        //public PersonsDto Persons { get; set; }
        //public LocationsDto Locations { get; set; }
    }
}
