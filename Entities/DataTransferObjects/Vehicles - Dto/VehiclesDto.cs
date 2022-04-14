namespace Back_End.Models.Vehicles___Dto
{
    public class VehiclesDto
    {
        public string VehiclePatent { get; set; }

        public int FK_TypeVehicleID { get; set; }

        public string Type { get; set; }

        public int VehicleYear { get; set; }


        public int FK_EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        public int FK_BrandID { get; set; }
        public int FK_ModelID { get; set; }
    }
}
