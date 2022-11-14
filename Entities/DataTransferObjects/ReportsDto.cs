using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.DataTransferObjects.Victims___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class ReportsDto
    {
        public int EmergencyDisasterID { get; set; }
        public DateTime EmergencyDisasterStartDate { get; set; }
        public string? EmergencyDisasterEndDate { get; set; }

        public string Type { get; set; }
        public string City { get; set; }

        public string State { get; set; }
        public string alertName { get; set; }
        public VictimsDto Victims { get; set; }
        public string Recurso { get; set; }

        //public ICollection<ResourcesRequestMaterialsMedicinesVehiclesDto> Resources_Requests { get; set; }

        public Dictionary<string, int> Recursos { get; set; }

        

    }


    public class Recursos
    {
        public int Materiales { get; set; }
        public int Medicamentos { get; set; }
        public int Vehiculos { get; set; }
    }
}
