using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.Models.Vehicles___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Materials___Dto
{
    public class MaterialsDto
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialMark { get; set; }

        public int MaterialQuantity { get; set; }

        public string MaterialUtility { get; set; }

        public Boolean MaterialAvailability { get; set; }

        public string MaterialPicture { get; set; }
        public EstatesVehiclesDto Estates { get; set; }
    }
}
