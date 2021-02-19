using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;
using RentalSystem.Models.Dtos;
using RentalSystem.Services;

namespace RentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
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
                return BadRequest("用户名不存在");
            if (user.Password != userModel.Password)
                return BadRequest("密码错误");
            return Ok("登录成功");
        }

        [HttpPost]
        public IActionResult Register([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("参数不合法");
            if (_userService.GetUserByUsername(userModel.Username) != null)
                return BadRequest("该用户名已被使用");
            if (_userService.AddUser(userModel) != 1)
                return BadRequest("注册失败");
            return Ok("注册成功");
        }

        [HttpPut]
        public IActionResult Reset([FromBody] UserInfoDto userInfoDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest("参数不合法");
            if (_userService.UpdateUserInfoByUserName(userInfoDtos) != 1)
                return BadRequest("用户信息更新失败");
            return Ok("用户信息更新成功");
        }
        
        [HttpPut("[action]")]
        public IActionResult ResetPassword([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("参数不合法");
            if (_userService.UpdateUserPassword(userModel) != 1)
                return BadRequest("用户信息更新失败");
            return Ok("用户信息更新成功");
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