using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Volunteers__Dto
{
   public class PersonsAppDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Boolean Status { get; set; } = true;
    }
}
