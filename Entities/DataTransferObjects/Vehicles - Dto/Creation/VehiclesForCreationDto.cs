namespace Entities.DataTransferObjects.Vehicles___Dto.Creation
{
    public class VehiclesForCreationDto
    {
        public string VehiclePatent { get; set; }

        public int VehicleYear { get; set; }

        public string VehicleUtility { get; set; }

        public int? FK_EmployeeID { get; set; }

        public int FK_TypeVehicleID { get; set; }

        public int FK_BrandID { get; set; }
        public int FK_ModelID { get; set; }

        //public TypeVehiclesForCreationDto TypeVehicles { get; set; }
        //public BrandsModelsForCreationDto BrandsModels { get; set; }

    }
}
