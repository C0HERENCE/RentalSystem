using Microsoft.AspNetCore.Mvc;

namespace RentalSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        public TestController()
        {
            
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Test()
        {
            return Ok("ok...");
        }
    }
}