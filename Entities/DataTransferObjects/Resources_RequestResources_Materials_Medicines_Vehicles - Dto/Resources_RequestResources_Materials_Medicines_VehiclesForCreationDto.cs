namespace Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto
{
    public class Resources_RequestResources_Materials_Medicines_VehiclesForCreationDto
    {
        public string? FK_MaterialID {get; set;}
        public string? FK_MedicineID { get; set;}

        public string? FK_VehicleID { get; set; }

        public int Quantity { get; set; }
    }
}
