using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IMaterialsRepository : IRepositoryBase<Materials>
    {
        Task<IEnumerable<Materials>> GetAllMaterials();

        Task<Materials> GetMaterialById(string materialId);

        Task<Materials> GetMaterialWithDetails(string materialId);

        void CreateMaterial(Materials material);
        void UpdateMaterial(Materials material);

        void DeleteMaterial(Materials material);

    }
}
