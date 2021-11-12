using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Volunteers__Dto
{
    public class VolunteersUsersAppDto
    {
        public string UserDni { get; set; }
        public PersonsAppDto Persons { get; set; }
    }
}
