using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ITypesEmergenciesDisastersRepository : IRepositoryBase<TypesEmergenciesDisasters>
    {
        Task<IEnumerable<TypesEmergenciesDisasters>> GetAllTypesEmergenciesDisasters();
        Task<TypesEmergenciesDisasters> GetTypeEmergencyDisaster(int TypeEmergencyDisasterId);
    }
}
