using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RentalServer.Data;
using RentalServer.Model;

namespace RentalServer.Controllers
{
    [ApiController]
    [Route("/api/class")]
    public class ClassController
    {
        private readonly RentalDbContext _dbContext;

        public ClassController(RentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        [Route("get")]
        public CommonResult<List<Class>> GetClass()
        {
            return CommonResult.Success(_dbContext.Classes.ToList());
        }
    }
}