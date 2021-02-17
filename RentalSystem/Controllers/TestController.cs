using Microsoft.AspNetCore.Mvc;

namespace RentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        public TestController()
        {
            
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("ok...");
        }
    }
}