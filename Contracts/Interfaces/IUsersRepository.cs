using Back_End.Models;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IUsersRepository : IRepositoryBase<Users>
    {

        Task<Users> GetUserEmployeeById(int userId);

        Task<Users> GetUserVolunteerById(int userId);

        void DeletUser(Users user);
        void ForgotPassword(ForgotPasswordRequest model);
        void ResetPassword(string token, ResetPasswordRequest model);

        Task<UserEmployeeAuthDto> ValidateUser(UserLoginDto user);
    }
}
