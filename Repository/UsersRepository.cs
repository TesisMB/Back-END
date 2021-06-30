using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        public UsersRepository(CruzRojaContext cruzRojaContext2)
            : base(cruzRojaContext2)
        {

        }

        public Users GetUserEmployeeById(int userId)
        {
            return FindByCondition(emp => emp.UserID.Equals(userId))
                    .Include(a => a.Employees)
                    .Include(a => a.Persons)
                    .FirstOrDefault();
        }

        public Users GetUserVolunteerById(int userId)
        {
            return FindByCondition(emp => emp.UserID.Equals(userId))
                              .Include(a => a.Volunteers)
                              .Include(a => a.Persons)
                              .Include(a => a.Volunteers.VolunteersSkills)
                              .ThenInclude(a => a.Skills)
                              .FirstOrDefault();
        }


        public void DeletUser(Users user)
        {
            Delete(user);
        }

    }
}
