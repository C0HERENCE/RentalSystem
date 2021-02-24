using System.Data.SqlClient;
using Dapper;
using RentalSystem.Models;
using RentalSystem.Models.Dtos;

namespace RentalSystem.Dao
{
    public class UserDao : IUserDao
    {
        private readonly SqlConnection _sqlConnection;

        public UserDao(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }
        
        public int AddUser(UserModel user)
        {
            const string sql = "insert into [user] values (@description, @username, @password, @is_admin, @enabled)";
            return _sqlConnection.Execute(sql, new
            {
                description = user.Description,
                username = user.Username,
                password = user.Password,
                is_admin = user.IsAdmin,
                enabled = user.Enabled
            });
        }

        public int RemoveUser(int id)
        {
            const string sql = "update [user] set enabled = @enabled where id = @uid";
            return _sqlConnection.Execute(sql, new {enabled = 0, uid = id});
        }

        public int UpdateUser(UserInfoDto user)
        {
            const string sql = "update [user] set description = @description where id = @uid";
            return _sqlConnection.Execute(sql, new {description = user.Description, uid = user.Id});
        }

        public UserModel GetUserById(int id)
        {
            const string sql = "select * from [user] where id = @uid";
            return _sqlConnection.QuerySingleOrDefault<UserModel>(sql, new {uid = id});
        }

        public UserModel GetUserByUsername(string username)
        {
            const string sql = "select * from [user] where username = @uname";
            return _sqlConnection.QuerySingleOrDefault<UserModel>(sql, new {uname = username});
        }

        public int UpdatePassword(UserModel user)
        {
            const string sql = "update [user] set password = @password where id = @uid";
            return _sqlConnection.Execute(sql, new {password = user.Password, uid = user.Id});
        }
    }
}