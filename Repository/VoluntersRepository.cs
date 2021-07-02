using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class VoluntersRepository : RepositoryBase<Volunteers>, IVolunteersRepository
    {
        public VoluntersRepository(CruzRojaContext cruzRojaContext2)
        : base(cruzRojaContext2)
        {

        }
        public IEnumerable<Volunteers> GetAllVolunteers()
        {
            return FindAll()
                    .Include(a => a.Users)
                    .ThenInclude(a => a.Persons)
                    .Include(a => a.Users.Roles)
                    .Include(a => a.Users.Estates)
                    .ThenInclude(a => a.LocationAddress)
                    .Include(a => a.Users.Estates.EstatesTimes)
                    .ThenInclude(a => a.Times)
                    .ThenInclude(a => a.Schedules)
                    .Include(a => a.VolunteersSkills)
                   .ThenInclude(a => a.Skills)
                    .ToList();
        }

        public Volunteers GetVolunteersById(int volunteerId)
        {
            return FindByCondition(volunteer => volunteer.VolunteerID.Equals(volunteerId))
                .Include(a => a.Users)
                .ThenInclude(a => a.Persons)
                .FirstOrDefault();
        }


        public void CreateVolunteer(Volunteers volunteer)
        {
            Create(volunteer);
        }
        public void UpdateVolunteer(Volunteers volunteer)
        {
            Update(volunteer);
        }

        public Volunteers GetVolunteerWithDetails(int volunteerId)
        {
            return FindByCondition(volunteer => volunteer.VolunteerID.Equals(volunteerId))
                    .Include(a => a.Users)
                    .ThenInclude(a => a.Persons)
                    .Include(a => a.Users.Roles)
                    .Include(a => a.Users.Estates)
                    .ThenInclude(a => a.LocationAddress)
                    .Include(a => a.Users.Estates.EstatesTimes)
                    .ThenInclude(a => a.Times)
                    .ThenInclude(a => a.Schedules)
                    .Include(a => a.VolunteersSkills)
                   .ThenInclude(a => a.Skills)
                   .FirstOrDefault();
        }
    }
}
