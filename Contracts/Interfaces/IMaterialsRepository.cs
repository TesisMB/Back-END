using Back_End.Models;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IMaterialsRepository : IRepositoryBase<Materials>
    {
        Task<IEnumerable<Materials>> GetAllMaterials(int userId);
        IEnumerable<Materials> GetAllMaterials(DateTime dateStart, DateTime dateEnd);

        Task<Materials> GetMaterialById(string materialId);

        Task<Materials> GetMaterialWithDetails(string materialId);

        void CreateMaterial(Materials material);
        void UpdateMaterial(Materials material);

        void DeleteMaterial(Materials material);
        IEnumerable<Materials> GetAllMaterials(DateTime dateConvert, DateTime dateConvertEnd, int estateId);
    }
}
