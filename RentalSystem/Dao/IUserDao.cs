using RentalSystem.Models;
using RentalSystem.Models.Dtos;

namespace RentalSystem.Dao
{
    public interface IUserDao
    {
        int AddUser(UserModel user);
        int RemoveUser(int id);
        int UpdateUser(UserInfoDto user);
        UserModel GetUserById(int id);
        UserModel GetUserByUsername(string username);
        int UpdatePassword(UserModel userModel);
    }
}