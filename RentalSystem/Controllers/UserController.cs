using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;
using RentalSystem.Models.Dtos;
using RentalSystem.Services;

namespace RentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] UserModel userModel)
        {
            var user = _userService.GetUserByUsername(userModel.Username);
            if (user == null)
                return Failed("用户名不存在");
            if (user.Password != userModel.Password)
                return Failed("密码错误");
            return Success("登录成功");
        }

        [HttpPost]
        public IActionResult Register([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
                return Failed("参数不合法");
            if (_userService.GetUserByUsername(userModel.Username) != null)
                return Failed("该用户名已被使用");
            userModel.Enabled = 1; // 注册用户默认为启用状态
            if (_userService.AddUser(userModel) != 1)
                return Failed("注册失败");
            return Success("注册成功");
        }

        [HttpPut]
        public IActionResult Reset([FromBody] UserInfoDto userInfoDtos)
        {
            if (!ModelState.IsValid)
                return Failed("参数不合法");
            if (_userService.UpdateUserInfoById(userInfoDtos) != 1)
                return Failed("用户信息更新失败");
            return Success("用户信息更新成功");
        }
        
        [HttpPut("[action]")]
        public IActionResult ResetPassword([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
                return Failed("参数不合法");
            if (_userService.UpdateUserPassword(userModel) != 1)
                return Failed("用户信息更新失败");
            return Success("用户信息更新成功");
        }

        [HttpGet("{id}")]
        public IActionResult GetUserInfo(int id)
        {
            var userModel = _userService.GetUserById(id);
            if (userModel == null)
                return BadRequest("用户不存在");
            var infoDto = new UserInfoDto {Id = userModel.Id, Username = userModel.Username, Description = userModel.Description};
            return Ok(infoDto);
        }
    }
}