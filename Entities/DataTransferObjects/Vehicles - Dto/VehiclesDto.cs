namespace Back_End.Models.Vehicles___Dto
{
    public class VehiclesDto
    {
        public string VehiclePatent { get; set; }
        public string VehicleYear { get; set; }
        public string VehicleUtility { get; set; }

        public int Fk_TypeVehicleID { get; set; }
        public string Type { get; set; }

        public int FK_EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        public int FK_BrandID { get; set; }
        public string BrandName { get; set; }
        
        public int FK_ModelID { get; set; }
        public string ModelName { get; set; }


    }
}
