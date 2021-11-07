using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Resources_Request___Dto
{
    public class ResourcesForCreationDto
    {
        public Resources_MaterialsForCreationDto Resources_Materials { get; set; }

        public Resources_MedicinesForCreationDto Resources_Medicines { get; set; }

        public Resources_VehiclesForCreationDto Resources_Vehicles { get; set; }
    }
}
