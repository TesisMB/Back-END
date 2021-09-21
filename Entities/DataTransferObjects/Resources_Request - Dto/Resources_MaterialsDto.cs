using Entities.DataTransferObjects.Materials___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Resources_Request___Dto
{
    public class Resources_MaterialsDto
    {
        public int ID { get; set; }

        public int Quantity { get; set; }

        public MaterialsDto Materials { get; set; }
    }
}
