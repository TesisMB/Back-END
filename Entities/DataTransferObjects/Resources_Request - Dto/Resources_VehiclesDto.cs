using Back_End.Models.Vehicles___Dto;

namespace Entities.DataTransferObjects.Resources_Request___Dto
{
    public class Resources_VehiclesDto
    {
        public int ID { get; set; }

        public int Quantity { get; set; }

        public VehiclesDto Vehicles { get; set; }
    }
}
