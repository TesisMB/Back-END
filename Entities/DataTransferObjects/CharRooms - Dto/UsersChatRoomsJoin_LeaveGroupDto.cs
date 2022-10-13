namespace Entities.DataTransferObjects.CharRooms___Dto
{
    public class UsersChatRoomsJoin_LeaveGroupDto
    {
        public int FK_UserID { get; set; }
        public int FK_ChatRoomID { get; set; }
        public bool Status { get; set; } = false;
        public Coords Coords { get; set; }

    }



    public class Coords
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
