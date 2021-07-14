using Entities.Models;
using System.Collections.Generic;

namespace Contracts.Interfaces
{
    public interface IMaterialsRepository : IRepositoryBase<Materials>
    {
        IEnumerable<Materials> GetAllMaterials();

        Materials GetMaterialById(int materialId);

        Materials GetMaterialWithDetails(int materialId);

        void CreateMaterial(Materials material);
        void UpdateMaterial(Materials material);

        void DeleteMaterial(Materials material);

    }
}
