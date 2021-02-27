using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Controllers
{
    public enum ResponseCode
    {
        SUCCESS = 200,
        BADREQUEST = 400,
        NOTFOUND = 404,
        ERROR = 500,
    }
    public class CommonResult : ActionResult
    {
        public ResponseCode Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        public CommonResult(ResponseCode status, string message="", object data=null)
        {
            Status = status;
            Result = data;
            Message = message;
        }

        public CommonResult() : this (0, "", null)
        {
        }
    }
    public class BaseController : Controller
    {
        [NonAction]
        public IActionResult Success(string message, object data = null) => new ObjectResult(new CommonResult(ResponseCode.SUCCESS, message, data));

        [NonAction]
        public IActionResult BadRequest(string message, object data = null) => new ObjectResult(new CommonResult(ResponseCode.BADREQUEST, message, data));
        [NonAction]
        public IActionResult NotFound(string message, object data = null) => new ObjectResult(new CommonResult(ResponseCode.NOTFOUND, message, data));
        [NonAction]
        public IActionResult Error(string message, object data = null) => new ObjectResult(new CommonResult(ResponseCode.ERROR, message, data));
    }
}