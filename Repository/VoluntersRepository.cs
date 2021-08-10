using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Volunteers__Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class VoluntersRepository : RepositoryBase<Volunteers>, IVolunteersRepository
    {
        private CruzRojaContext cruzRojaContext;

        public VoluntersRepository(CruzRojaContext cruzRojaContext2)
        : base(cruzRojaContext2)
        {
            cruzRojaContext = cruzRojaContext2;
        }
        public async Task<IEnumerable<Volunteers>> GetAllVolunteers()
        {
            return await FindAll()
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
                    .ToListAsync();
        }

        public async Task<IEnumerable<Volunteers>> GetAllVolunteersApp()
        {

            bool volunteer = true;
           
            var collection = cruzRojaContext.Volunteers as IQueryable<Volunteers>;

            if (volunteer == true)
            {
                collection = collection.Where(a => a.Users.Persons.Status == volunteer);
            }

            return await collection
                            .Include(a => a.Users)
                            .ThenInclude(a => a.Persons)
                            .Include(a => a.Users.Estates.EstatesTimes)
                            .ThenInclude(a => a.Times)
                            .ThenInclude(a => a.Schedules)
                            .Include(a => a.VolunteersSkills)
                            .ThenInclude(a => a.Skills)
                            .ToListAsync();

        }


        public async Task<Volunteers> GetVolunteersById(int volunteerId)
        {
            return await FindByCondition(volunteer => volunteer.VolunteerID.Equals(volunteerId))
                .Include(a => a.Users)
                .ThenInclude(a => a.Persons)
                .FirstOrDefaultAsync();
        }


        public void CreateVolunteer(Volunteers volunteer)
        {
            Create(volunteer);
        }
        public void UpdateVolunteer(Volunteers volunteer)
        {
            Update(volunteer);
        }

        public async Task<Volunteers> GetVolunteerWithDetails(int volunteerId)
        {
            return await FindByCondition(volunteer => volunteer.VolunteerID.Equals(volunteerId))
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
                   .FirstOrDefaultAsync();
        }

      
    }
}
