using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using System;
using System.Collections.Generic;

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

        public List<EmergenciesDisasterByUser> EmergencyDisastersReports { get; set; }
    }


    public class EmergenciesDisasterByUser
    {
        public int ID { get; set; }

        public string State { get; set; }

        public string Type{ get; set; }


        public string Degree { get; set; }
        public string City { get; set; }

    }
}
