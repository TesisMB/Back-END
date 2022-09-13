namespace Entities.DataTransferObjects.Victims___Dto
{
    public class VictimsForUpdateDto
    {
        public int? NumberDeaths { get; set; }

        public int? NumberAffected { get; set; }

        public int? NumberFamiliesAffected { get; set; }

        public decimal? MaterialsDamage { get; set; }

        public int? AffectedLocalities { get; set; }

        public int? EvacuatedPeople { get; set; }

        public int? AffectedNeighborhoods { get; set; }

        public int? AssistedPeople { get; set; }
        public int? RecoveryPeople { get; set; }
    }
}
