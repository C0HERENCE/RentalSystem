using RentalSystem.Dao;
using RentalSystem.Models;
using RentalSystem.Models.Dtos;

namespace RentalSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDao _userDao;

        public UserService(IUserDao userDao)
        {
            _userDao = userDao;
        }
        public UserModel GetUserById(int id)
        {
            return _userDao.GetUserById(id);
        }

        public UserModel GetUserByUsername(string username)
        {
            return _userDao.GetUserByUsername(username);
        }

        public int AddUser(UserModel userModel)
        {
            return _userDao.AddUser(userModel);
        }

        public int UpdateUserInfoByUserName(UserInfoDto userInfoDto)
        {
            return _userDao.UpdateUser(userInfoDto);
        }

        public int UpdateUserPassword(UserModel userModel)
        {
            return _userDao.UpdatePassword(userModel);
        }
    }
}