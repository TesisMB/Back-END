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
        private CruzRojaContext _cruzRojaContext;

        public VoluntersRepository(CruzRojaContext cruzRojaContext)
        : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }

        public async Task<IEnumerable<Volunteers>> GetAllVolunteers()
        {

            var volunteers = UsersRepository.authUser;

            var collection = _cruzRojaContext.Volunteers as IQueryable<Volunteers>;

            collection = collection.Where(
                                        a => a.Users.Estates.Locations.LocationDepartmentName == volunteers.Estates.Locations.LocationDepartmentName);

            return await collection
                         .Include(a => a.Users)
                         .ThenInclude(a => a.Persons)
                         .Include(a => a.Users.Locations)
                         .Include(a => a.Users.Roles)
                         .Include(a => a.Users.Estates)
                         .ThenInclude(a => a.LocationAddress)
                         .Include(a => a.Users.Estates.EstatesTimes)
                         .ThenInclude(a => a.Times)
                         .ThenInclude(a => a.Schedules)
                         .Include(a => a.Users.Estates.Locations)
                         .Include(a => a.VolunteersSkills)
                         .ThenInclude(a => a.Skills)
                         .Include(a => a.VolunteersSkills)
                         .ThenInclude(a => a.VolunteersSkillsFormationEstates)
                         .ThenInclude(a => a.FormationsEstates)
                         .ThenInclude(a => a.FormationsDates)
                         .ToListAsync();
        }

        public async Task<IEnumerable<Volunteers>> GetAllVolunteersApp()
        {
            return await FindAll()
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
            return await FindByCondition(volunteer => volunteer.ID.Equals(volunteerId))
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
            return await FindByCondition(volunteer => volunteer.ID.Equals(volunteerId))
                    .Include(a => a.Users)
                    .ThenInclude(a => a.Persons)
                    .Include(a => a.Users.Locations)
                    .Include(a => a.Users.Roles)
                    .Include(a => a.Users.Estates)
                    .ThenInclude(a => a.LocationAddress)
                    .Include(a => a.Users.Estates.EstatesTimes)
                    .ThenInclude(a => a.Times)
                    .ThenInclude(a => a.Schedules)
                    .Include(a => a.Users.Estates.Locations)
                    .Include(a => a.VolunteersSkills)
                    .ThenInclude(a => a.Skills)
                    .Include(a => a.VolunteersSkills)
                    .ThenInclude(a => a.VolunteersSkillsFormationEstates)
                    .ThenInclude(a => a.FormationsEstates)
                    .ThenInclude(a => a.FormationsDates)
                    .FirstOrDefaultAsync();
        }

        public async Task<Volunteers> GetVolunteerAppWithDetails(int volunteerId)
        {
               return await FindByCondition(volunteer => volunteer.ID.Equals(volunteerId))
                     .Include(a => a.Users)
                     .ThenInclude(a => a.Persons)
                     .Include(a => a.Users.Estates.EstatesTimes)
                     .ThenInclude(a => a.Times)
                     .ThenInclude(a => a.Schedules)
                     .Include(a => a.VolunteersSkills)
                     .ThenInclude(a => a.Skills)
                     .FirstOrDefaultAsync();
        }
    }
}
