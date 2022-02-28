using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IEmergenciesDisastersRepository : IRepositoryBase<EmergenciesDisasters>
    {
        Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisasters();
        Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersWithourFilter();


        Task<EmergenciesDisasters> GetEmergencyDisasterById(int emergencydisasterId);

        Task<EmergenciesDisasters> GetEmergencyDisasterWithDetails(int emergencydisasterId);

        void CreateEmergencyDisaster(EmergenciesDisasters emergencyDisaster);
        void UpdateEmergencyDisaster(EmergenciesDisasters emergencyDisaster);

        void DeleteEmergencyDisaster(EmergenciesDisasters emergencyDisaster);

    }
}
