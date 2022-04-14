using Back_End.Models;
using Entities.DataTransferObjects.BrandsModels__Dto;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ITypesVehicles : IRepositoryBase<TypeVehicles>
    {
        Task<IEnumerable<TypeVehicles>> GetAllTypesVehicles();

        //Task<IEnumerable<BrandsModels>> GetFilterTypesVehicles(BrandsModelsForSelectDto brandsModels);

    }
}
