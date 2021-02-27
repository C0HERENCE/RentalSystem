using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Dao;

namespace RentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : BaseController
    {
        private readonly RentalSystemDbContext _dbContext;

        public BrandController(RentalSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var brand = _dbContext.Brands.FirstOrDefault(b => b.Id == id);
            return brand == null
                ? NotFound("该商品型号不存在")
                : Success("ok", brand);
        }

        [HttpGet]
        public IActionResult GetAllBrands()
        {
            return Success("ok", _dbContext.Brands.ToList());
        }
    }
}