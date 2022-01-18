using Entities.DataTransferObjects.FormationsDates___Dto;
using Entities.DataTransferObjects.FormationsEstates___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.VolunteersSkillsFormationEstates
{
    public class VolunteersSkillsFormationEstatesDto
    {
        public int FormationEstateID { get; set; }

        public string FormationEstateName { get; set; }

        public int FormationDateID { get; set; }

        public string Date { get; set; }
    }
}
