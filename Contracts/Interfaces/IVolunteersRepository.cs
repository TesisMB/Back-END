
using Back_End.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IVolunteersRepository : IRepositoryBase<Volunteers>
    {
        Task<IEnumerable<Volunteers>> GetAllVolunteers();

        Task<IEnumerable<Volunteers>> GetAllVolunteersApp();

        Task<Volunteers> GetVolunteersById(int volunteerId);

        Task<Volunteers> GetVolunteerWithDetails(int volunteerId);

        void CreateVolunteer(Volunteers volunteer);
        void UpdateVolunteer(Volunteers volunteer);
    }
}
