using Entities.DataTransferObjects.FormationsDates___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.FormationsEstates___Dto
{
    public class FormationsEstatesForCreationDto
    {
        public string FormationEstateName { get; set; }

        public FormationsDatesForCreationDto FormationsDates { get; set; }

        public int FK_FormationDateID { get; set; }
    }
}
