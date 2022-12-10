using Back_End.Models;
using Entities.DataTransferObjects.Materials___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IMaterialsRepository : IRepositoryBase<Materials>
    {
        Task<IEnumerable<Materials>> GetAllMaterials(int userId, int locationId);
        Task<IEnumerable<Materials>> GetAllMaterials();

        Task<Materials> GetMaterialById(string materialId);

        Task<Materials> GetMaterialWithDetails(string materialId);

        void CreateMaterial(Materials material, Resources_ForCreationDto resources);
        void UpdateMaterial(Materials material, JsonPatchDocument<MaterialsForUpdateDto> _materials, MaterialsForUpdateDto materialToPatch);

        void DeleteMaterial(Materials material);
        IEnumerable<Materials> GetAllMaterials(DateTime dateConvert, DateTime dateConvertEnd, int estateId);
    }
}
