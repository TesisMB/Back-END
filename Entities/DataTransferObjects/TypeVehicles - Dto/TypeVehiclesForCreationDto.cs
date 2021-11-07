using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.TypeVehicles___Dto
{
    public class TypeVehiclesForCreationDto
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Mark { get; set; }

        [Required]
        public string Model { get; set; }
    }
}
