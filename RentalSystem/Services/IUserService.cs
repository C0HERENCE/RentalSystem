using RentalSystem.Models;
using RentalSystem.Models.Dtos;

namespace RentalSystem.Services
{
    public interface IUserService
    {
        UserModel GetUserById(int id);
        UserModel GetUserByUsername(string username);
        int AddUser(UserModel userModel);
        int UpdateUserInfoById(UserInfoDto userModel);
        int UpdateUserPassword(UserModel userModel);
    }
}