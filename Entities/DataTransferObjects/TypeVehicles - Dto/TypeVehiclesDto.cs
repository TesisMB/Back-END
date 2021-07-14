using Entities.DataTransferObjects.Marks___Dto;
using Entities.DataTransferObjects.MarksModels___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.TypeVehicles___Dto
{
    public class TypeVehiclesDto
    {
        public string Type { get; set; }

        public string Mark { get; set; }
        public string Model { get; set; }
        //public ICollection<TypeVehiclesModelsDto> TypeVehiclesModels { get; set; }
    }
}
