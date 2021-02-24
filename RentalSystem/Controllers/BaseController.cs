using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Controllers
{
    public class CommonResult : ActionResult
    {
        public int Status { get; set; }
        public object Result { get; set; }

        public CommonResult(int status, object data)
        {
            Status = status;
            Result = data;
        }

        public CommonResult() : this (0, null)
        {
        }
    }
    public class BaseController : Controller
    {
        [NonAction]
        public IActionResult Success(object data = null) => new ObjectResult(new CommonResult(1, data));

        [NonAction]
        public IActionResult Failed(object data = null) => new ObjectResult(new CommonResult(0, data));
    }
}