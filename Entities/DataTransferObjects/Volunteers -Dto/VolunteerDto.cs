using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Volunteers__Dto
{
   public  class VolunteerDto
    {
        public string Dni { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Address { get; set; }
        public Boolean Status { get; set; }
    }
}
