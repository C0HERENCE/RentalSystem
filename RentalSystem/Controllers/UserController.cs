using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Dao;
using RentalSystem.Models;
using RentalSystem.Services;

namespace RentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly RentalSystemDbContext _dbContext;
        private readonly JwtService _jwt;

        public UserController(RentalSystemDbContext dbContext, JwtService jwtService)
        {
            _dbContext = dbContext;
            _jwt = jwtService;
        }
        
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] dynamic userRequest)
        {
            string username = userRequest.username;
            string password = userRequest.password;
            
            if (_dbContext.Users.Count(u => u.Username == username) != 1)
                return NotFound("用户名不存在");
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user ==
                   null
                ? BadRequest("密码错误")
                : Success("登录成功", _jwt.GenerateToken(user.Id));
        }

        [HttpPost]
        public IActionResult Register([FromBody] dynamic user)
        {
            var userAdd = new User
            {
                Description = "",
                Enabled = 1,
                Password = user.password,
                Username = user.username,
                IsAdmin = 0
            };

            if (_dbContext.Users.Count(u => u.Username == userAdd.Username) != 0)
                return BadRequest("该用户名已被使用");
            return _dbContext.Users.Add(userAdd).Entity == null ? BadRequest("注册失败") : Success("注册成功");
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] dynamic userRequest)
        {
            var id = JwtService.GetUserId(HttpContext);
            var user = _dbContext.Users.FirstOrDefault(u=>u.Id==id);
            if (user == null) return NotFound("用户不存在");
            user.Description = userRequest.description;
            try
            {
                _dbContext.Update(user);
                _dbContext.SaveChanges();
                return Success("用户信息更新成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Error("用户信息更新失败");
            }
        }
        
        [Authorize]
        [HttpPut("password")]
        public IActionResult ResetPassword([FromBody] dynamic userRequest)
        {
            var id = JwtService.GetUserId(HttpContext);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("用户不存在");
            string oldPassword = userRequest.old_password;
            user.Password = userRequest.new_password;
            if (_dbContext.Users.Count(u=>u.Password == oldPassword && u.Id == user.Id) != 1)
                return BadRequest("原密码错误");
            try
            {
                _dbContext.Update(user);
                _dbContext.SaveChanges();
                return Success("用户密码更新成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Error("用户密码更新失败");
            }
        }
        
        [HttpGet]
        public IActionResult GetUser(int? id)
        {
            id ??= JwtService.GetUserId(HttpContext);
            var user = _dbContext.Users.FirstOrDefault(u=>u.Id == id);
            if (user == null)
                return NotFound("用户不存在");
            return Success("ok", new
            {
                id = user.Id,
                username = user.Username,
                description = user.Description,
            });
        }
    }
}