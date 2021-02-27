using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Dao;
namespace RentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        private readonly RentalSystemDbContext _dbContext;

        public CategoryController(RentalSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetChildCategories(int id)
        {
            return _dbContext.Categories.Count(c => c.Id == id) == 0
                ? NotFound("分类不存在")
                : Success("ok", _dbContext.Categories.Where(c => c.ParentId == id).ToList());
        }
        
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Success("ok", _dbContext.Categories.Where(c => c.ParentId == 0).ToList());
        }
    }
}