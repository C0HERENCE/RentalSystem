using Microsoft.AspNetCore.Mvc;

namespace RentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public UserController()
        {
            
        }
        
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult Login()
        {
            return Ok("123");
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register()
        {
            return Ok("456");
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Reset()
        {
            return Ok("789");
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok("1234");
        }
    }
}