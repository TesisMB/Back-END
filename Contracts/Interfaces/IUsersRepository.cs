using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Interfaces
{
    public interface IUsersRepository : IRepositoryBase<Users>
    {

        Users GetUserEmployeeById(int userId);

        Users GetUserVolunteerById(int userId);

        void DeletUser(Users user);
    }
}
