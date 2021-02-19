using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using RentalSystem.Models;
using RentalSystem.Models.Dtos;

namespace RentalSystem.Dao
{
    public class UserDao : IUserDao
    {
        private readonly string _connectionString;
        public UserDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RentalSystem");
        }
        
        public int AddUser(UserModel user)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            const string sql = "insert into [user] values (@description, @username, @password, @is_admin, @enabled)";
            return sqlConnection.Execute(sql, new
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
            using var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            const string sql = "update [user] set enabled = @enabled where id = @uid";
            return sqlConnection.Execute(sql, new {enabled = 0, uid = id});
        }

        public int UpdateUser(UserInfoDto user)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            const string sql = "update [user] set description = @description where id = @uid";
            return sqlConnection.Execute(sql, new {description = user.Description, uid = user.Id});
        }

        public UserModel GetUserById(int id)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            const string sql = "select * from [user] where id = @uid";
            return sqlConnection.QuerySingleOrDefault<UserModel>(sql, new {uid = id});
        }

        public UserModel GetUserByUsername(string username)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            const string sql = "select * from [user] where username = @uname";
            return sqlConnection.QuerySingleOrDefault<UserModel>(sql, new {uname = username});
        }

        public int UpdatePassword(UserModel user)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            const string sql = "update [user] set password = @password where id = @uid";
            return sqlConnection.Execute(sql, new {password = user.Password, uid = user.Id});
        }
    }
}