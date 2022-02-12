using Back_End.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Resources_Request___Dto;

namespace Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto
{
    public class Resources_RequestResources_Materials_Medicines_VehiclesDto
    {
        public string ID { get; set; }
        public int FK_MaterialID { get; set; }
        public int FK_MedicineID { get; set; }
        public int FK_VehicleID { get; set; }
        public int Quantity { get; set; }
    }
}
