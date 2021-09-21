using Back_End.Models.Users___Dto.Users___Persons;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Resources_Request___Dto
{
    public class Resources_RequestDto
    {
        public int ID { get; set; }

        public DateTime RequestDate { get; set; }

        public string Reason { get; set; }

        public bool Status { get; set; }

        public EmployeesUsersDto Users { get; set; }

        public EmergenciesDisastersDto EmergenciesDisasters { get; set; }

        public ResourcesDto Resources { get; set; }
    }
}
