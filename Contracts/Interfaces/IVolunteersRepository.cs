
using Back_End.Models;
using System.Collections.Generic;

namespace Contracts.Interfaces
{
    public interface IVolunteersRepository : IRepositoryBase<Volunteers>
    {
        IEnumerable<Volunteers> GetAllVolunteers();

        Volunteers GetVolunteersById(int volunteerId);

        Volunteers GetVolunteerWithDetails(int volunteerId);


        void CreateVolunteer(Volunteers volunteer);
        void UpdateVolunteer(Volunteers volunteer);
    }
}
