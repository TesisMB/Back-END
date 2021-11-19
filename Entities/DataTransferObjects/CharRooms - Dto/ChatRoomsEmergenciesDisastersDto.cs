using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.CharRooms___Dto
{
    public class ChatRoomsEmergenciesDisastersDto
    {
        public int EmergencyDisasterID { get; set; }

        public string LocationCityName { get; set; }

        public int TypeEmergencyDisasterID { get; set; }

        public string TypeEmergencyDisasterName { get; set; }

        public string TypeEmergencyDisasterIcon { get; set; }
    }
}
