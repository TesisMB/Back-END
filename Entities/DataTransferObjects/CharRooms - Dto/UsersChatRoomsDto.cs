using Entities.DataTransferObjects.Messages___Dto;

namespace Entities.DataTransferObjects.CharRooms___Dto
{
    public class UsersChatRoomsDto
    {
        public int UserID { get; set; }
        public string Name { get; set; }

        public string UserDni { get; set; }
        public string RoleName { get; set; }
        public string Avatar { get; set; }
    }
}
