using Entities.DataTransferObjects.Medicines___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Resources_Request___Dto
{
   public class Resources_MedicinesDto
    {
        public int ID { get; set; }

        public int Quantity { get; set; }

        public MedicinesDto Medicines { get; set; }
    }
}
