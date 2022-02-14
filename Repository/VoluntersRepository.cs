using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Helpers;
using Entities.Models;
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
                            .Include(a => a.VolunteersSkills)
                            .ThenInclude(a => a.VolunteersSkillsFormationEstates)
                            .ThenInclude(a => a.FormationsEstates)
                            .ThenInclude(a => a.FormationsDates)
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

            Email.generatePassword(volunteer.Users);

            Email.sendVerificationEmail(volunteer.Users);
            SpaceCamelCase(volunteer);

            volunteer.Users.UserPassword = Encrypt.GetSHA256(volunteer.Users.UserPassword);



            Create(volunteer);
        }

        private void SpaceCamelCase(Volunteers volunteer)
        {
            //VER LA NUEVA CONTRASEÑA
            volunteer.VolunteerDescription = WithoutSpace_CamelCase.GetCamelCase(volunteer.VolunteerDescription);

            foreach (var vol in volunteer.VolunteersSkills)
            {

                foreach (var v in vol.VolunteersSkillsFormationEstates)
                {
                    if (vol.Skills != null)
                    {
                        vol.Skills.SkillName = WithoutSpace_CamelCase.GetCamelCase(vol.Skills.SkillName);
                        vol.Skills.SkillDescription = WithoutSpace_CamelCase.GetCamelCase(vol.Skills.SkillDescription);
                    }

                    if (v.FormationsEstates != null)
                    {
                        v.FormationsEstates.FormationEstateName = WithoutSpace_CamelCase.GetCamelCase(v.FormationsEstates.FormationEstateName);
                    }

                }

            }

            volunteer.Users.UserDni = WithoutSpace_CamelCase.GetCamelCase(volunteer.Users.UserDni);
            volunteer.Users.UserPassword = WithoutSpace_CamelCase.GetWithoutSpace(volunteer.Users.UserPassword);
            volunteer.Users.Persons.FirstName = WithoutSpace_CamelCase.GetCamelCase(volunteer.Users.Persons.FirstName);
            volunteer.Users.Persons.LastName = WithoutSpace_CamelCase.GetCamelCase(volunteer.Users.Persons.LastName);
            volunteer.Users.Persons.Phone = WithoutSpace_CamelCase.GetCamelCase(volunteer.Users.Persons.Phone);
            volunteer.Users.Persons.Address = WithoutSpace_CamelCase.GetCamelCase(volunteer.Users.Persons.Address);
            volunteer.Users.Persons.Email = WithoutSpace_CamelCase.GetWithoutSpace(volunteer.Users.Persons.Email);

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
                 .Include(a => a.VolunteersSkills)
                 .ThenInclude(a => a.VolunteersSkillsFormationEstates)
                 .ThenInclude(a => a.FormationsEstates)
                 .ThenInclude(a => a.FormationsDates)
                  .FirstOrDefaultAsync();
        }

        public static void CoordsLocation(LocationVolunteers location){
           
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            cruzRojaContext.Update(location);
        }       
    }
}
