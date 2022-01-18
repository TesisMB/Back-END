using Entities.DataTransferObjects.FormationsEstates___Dto;

namespace Entities.DataTransferObjects.VolunteersSkillsFormationEstates
{
    public class VolunteersSkillsFormationEstatesForCreationDto
    {
        public int FK_FormationEstateID { get; set; }
        public FormationsEstatesForCreationDto FormationsEstates { get; set; }
    }
}
