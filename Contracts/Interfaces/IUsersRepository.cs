using Back_End.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IUsersRepository : IRepositoryBase<Users>
    {

        Task<Users> GetUserEmployeeById(int userId);
        Task<Users> SendDeviceById(int userId);
        Task<Users> GetEmployeeVolunteerById(int userId);

        Task<Users> GetUsers(int userId);
        Task<IEnumerable<Users>> GetEmployeesVolunteers(int userId);

        Task<Users> GetUserVolunteerById(int userId);

        void DeletUser(Users user);
        void ForgotPassword(string email);
        void ResetPassword(string token, string password);
        Task<UserEmployeeAuthDto> ValidateUser(UserLoginDto user);
        void sendLoginNotification(Users account);
    }
}
