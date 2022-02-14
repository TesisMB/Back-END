namespace Entities.DataTransferObjects.CharRooms___Dto
{
    public class UsersChatRoomsJoin_LeaveGroupDto
    {
        public int FK_UserID { get; set; }
        public int FK_ChatRoomID { get; set; }

        public decimal LocationVolunteerLatitude { get; set; }
        public decimal LocationVolunteerLongitude { get; set; }

    }
}
