using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Resources_Request___Dto
{
   public class ResourcesDto
    {
        public int ID { get; set; }

        public Resources_MaterialsDto Resources_Materials { get; set; }

        public Resources_MedicinesDto Resources_Medicines { get; set; }

        public Resources_VehiclesDto Resources_Vehicles { get; set; }

    }
}
