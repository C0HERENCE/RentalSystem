using RentalSystem.Models;
using RentalSystem.Models.Dtos;

namespace RentalSystem.Services
{
    public interface IUserService
    {
        UserModel GetUserById(int id);
        UserModel GetUserByUsername(string username);
        int AddUser(UserModel userModel);
        int UpdateUserInfoByUserName(UserInfoDto userModel);
        int UpdateUserPassword(UserModel userModel);
    }
}