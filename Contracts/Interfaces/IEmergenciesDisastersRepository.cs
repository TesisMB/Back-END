using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IEmergenciesDisastersRepository : IRepositoryBase<EmergenciesDisasters>
    {
        Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisasters(int userId);
        Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersWithourFilter(int userId, string limit);
        Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersWithourFilterApp(int userId);
        Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersWithourFilterApp();

        Task<EmergenciesDisasters> GetEmergencyDisasterById(int emergencydisasterId);

        Task<EmergenciesDisasters> GetEmergencyDisasterWithDetails(int emergencydisasterId);

        void CreateEmergencyDisaster(EmergenciesDisasters emergencyDisaster);
        void UpdateEmergencyDisaster(EmergenciesDisasters emergencyDisaster, JsonPatchDocument<EmergenciesDisastersForUpdateDto> _emergencyDisaster,
                                     EmergenciesDisastersForUpdateDto emergenciesDisastersAnterior);

        void DeleteEmergencyDisaster(EmergenciesDisasters emergencyDisaster);

    }
}
