using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ITypesVehicles : IRepositoryBase<BrandsModels>
    {
        Task<IEnumerable<BrandsModels>> GetAllTypesVehicles();

    }
}
