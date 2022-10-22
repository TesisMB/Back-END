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

        public async Task<IEnumerable<Volunteers>> GetAllVolunteers(int userId)
        {

            //var volunteers = UsersRepository.authUser;

            var user =  EmployeesRepository.GetAllEmployeesById(userId);

            var collection = _cruzRojaContext.Volunteers as IQueryable<Volunteers>;

            collection = collection.Where(
                                        a => a.Users.Estates.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);

            return await collection
                         .Include(a => a.Users)
                         .Include(a => a.Users.Persons)
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

            sendVerificationEmail(volunteer.Users);
            SpaceCamelCase(volunteer);

            volunteer.Users.UserPassword = Encrypt.GetSHA256(volunteer.Users.UserPassword);



            Create(volunteer);
        }


        public static void sendVerificationEmail(Users user)
        {
            string message;


            message = $@"
              <div style='display: flex;
                                          justify-content: center;
                                          align-items: center;
                                          height: 96vh;'>

                    <div style='border: 1px solid #ccc;
                                max-width: 58%;
                                border-radius: 6px;
                                display: flex;
                                align-items: center;
                                justify-content: center;
                                flex-direction: column;
                                padding: 20px;'>

               <h2 style='font-weight: 400;
                          line-height: 30px;
                          font-size: 15px;
                          margin: 0 0 1rem 0;'>
                ¡Bienvenido a SINAGIR!</h2>


               <h3 stlye='font-weight: normal;
                    font-size: 13px;'>
                Tu cuenta se creo exitosamente</h3>


               <p   stlye='font-weight: normal;
                    font-size: 13px;'>
                Te dejamos los datos para que puedas acceder a tu cuenta</p>

                            <div class='datos' style='display: block;
                                                      width: 100%;'>

                                <p stlye='font-weight: normal;
                                   font-size: 13px;
                                   text-align: start;
                                   padding: 1rem 0 0 0;'>Usuario:  {user.UserDni }</p>

                                <p stlye='font-weight: normal;
                                   font-size: 13px;
                                   text-align: start;
                                   padding: 1rem 0 0 0;'>Contraseña: {user.UserPassword }</p>
                             </div>
                        </div>
                    </div>
             </div>";

            var msg = new Mail(new string[] { user.Persons.Email }, "Registro exitoso", $@"<p>Bienvenido a SICREYD!</p> {message}");

            EmailSender.SendEmail(msg);

            //Email.Send(
            //    to: user.Persons.Email,
            //    subject: "Sign-up Verification API",
            //    html: $@"<p>Bienvenido a SICREYD!</p>
            //             {message}"
            //);
        }

        private void SpaceCamelCase(Volunteers volunteer)
        {
            //VER LA NUEVA CONTRASEÑA
            volunteer.VolunteerDescription = WithoutSpace_CamelCase.GetWithoutSpace(volunteer.VolunteerDescription);

            foreach (var vol in volunteer.VolunteersSkills)
            {

                foreach (var v in vol.VolunteersSkillsFormationEstates)
                {
                    if (vol.Skills != null)
                    {
                        vol.Skills.SkillName = WithoutSpace_CamelCase.GetWithoutSpace(vol.Skills.SkillName);
                        vol.Skills.SkillDescription = WithoutSpace_CamelCase.GetWithoutSpace(vol.Skills.SkillDescription);
                    }

                    if (v.FormationsEstates != null)
                    {
                        v.FormationsEstates.FormationEstateName = WithoutSpace_CamelCase.GetWithoutSpace(v.FormationsEstates.FormationEstateName);
                    }

                }

            }

            volunteer.Users.UserDni = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(volunteer.Users.UserDni);
            volunteer.Users.UserPassword = WithoutSpace_CamelCase.GetWithoutSpace(volunteer.Users.UserPassword);
            volunteer.Users.Persons.FirstName = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(volunteer.Users.Persons.FirstName);
            volunteer.Users.Persons.LastName = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(volunteer.Users.Persons.LastName);
            volunteer.Users.Persons.Phone = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(volunteer.Users.Persons.Phone);
            volunteer.Users.Persons.Address = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(volunteer.Users.Persons.Address);
            volunteer.Users.Persons.Email = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(volunteer.Users.Persons.Email);

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

    }
}
