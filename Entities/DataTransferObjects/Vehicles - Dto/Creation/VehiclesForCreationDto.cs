namespace Entities.DataTransferObjects.Vehicles___Dto.Creation
{
    public class VehiclesForCreationDto
    {
        public string VehiclePatent { get; set; }

        public string VehicleYear { get; set; }

        public string VehicleUtility { get; set; }

        public int FK_EmployeeID { get; set; }
        public int Fk_TypeVehicleID { get; set; }

        public string BrandName { get; set; }
        public string ModelName { get; set; }

    }
}
