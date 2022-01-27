using Entities.DataTransferObjects.Resources_Request___Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto
{
    public class Resources_RequestResources_Materials_Medicines_VehiclesForCreationDto
    {
        public int? FK_MedicineID { get; set; }
        public int? FK_MaterialID { get; set; }
        //public int MaterialQuantity { get; set; }
        //public int MedicineQuantity { get; set; }
        public int? FK_VehiclesID { get; set; }

        public int? Quantity { get; set; }
        
    }
}
