using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RentalServer.Data;
using RentalServer.Model;

namespace RentalServer.Controllers
{
    [ApiController]
    [Route("/api/category")] 
    public class CategoryController
    {
        private readonly RentalDbContext _context;
        public CategoryController(RentalDbContext dbContext)
        {
            _context = dbContext;
        }
        
        [HttpGet]
        [Route("get")]
        public CommonResult<List<Category>> GetCategories()
        {
            return CommonResult.Success(_context.Categories.Where(c =>c.Enabled).ToList());
        }
    }
}