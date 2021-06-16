using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Entities;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Users___Dto.Users___Persons;

namespace Back_End.Models
{
    public class UsersDto
    {
        public int UserID { get; set; }
        public string UserDni { get; set; }
        public Boolean UserAvailable { get; set; }
        public PersonsDto Persons { get; set; }
        public EstatesDto Estates { get; set; }
    }
}
