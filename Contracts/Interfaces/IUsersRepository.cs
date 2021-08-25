using Back_End.Models;
using Entities.DataTransferObjects.Login___Dto;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IUsersRepository : IRepositoryBase<Users>
    {

        Task<Users> GetUserEmployeeById(int userId);

        Task<Users> GetUserVolunteerById(int userId);

        void DeletUser(Users user);
        void ForgotPassword(string email);
        void ResetPassword(string token, string password);

        UserEmployeeAuthDto ValidateUserE(Users user);

        Task<Users> ValidateUser(UserLoginDto user);

        UserVolunteerAuthDto ValidateUserV(Users user);

    }
}
