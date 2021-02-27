using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Dao;
using RentalSystem.Models;
using RentalSystem.Services;

namespace RentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoodsController : BaseController
    {
        private readonly RentalSystemDbContext _dbContext;
        private readonly JwtService _jwt;

        public GoodsController(RentalSystemDbContext dbContext, JwtService jwt)
        {
            _dbContext = dbContext;
            _jwt = jwt;
        }
        
        [HttpGet]
        public IActionResult GetByCategoryId([FromQuery]int categoryId, [FromQuery]int page = 0, [FromQuery]int limit = 20)
        {
            if (limit > 50)
                return BadRequest("传输数据量过大");
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
                return NotFound("分类不存在");
            if (category.ParentId == 0)
                return BadRequest("请传入一个子分类");
            return Success("ok",
                _dbContext.Goods.Where(g => g.CategoryId == categoryId)
                    .OrderBy(g => g.Id)
                    .Skip(page * limit)
                    .Take(limit).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var goods = _dbContext.Goods.FirstOrDefault(g => g.Id == id);
            return goods == null ? NotFound("商品不存在") : Success("ok", goods);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Publish([FromBody] dynamic goodsRequest)
        {
            var userId = _jwt.GetUserId(HttpContext);
            var good = new Good
            {
                UserId =  userId,
                BrandId =  0, // TODO 品牌功能,
                CategoryId = goodsRequest.category_id,
                Name = goodsRequest.name,
                Enabled = 1,
                OnSale = 1, // TODO 管理员审核功能
                Price = goodsRequest.price,
                Description = goodsRequest.description,
                OriginalPrice = goodsRequest.original_price,
                Stock = goodsRequest.stock,
                DetailHtml = goodsRequest.detail_html
            };
            var count = _dbContext.Categories.Count(c=>c.Id == good.CategoryId);
            if (count == 0)
                return BadRequest("分类不存在"); // TODO 分类已被禁用
            if (_dbContext.Goods.Count(g=>g.Name == good.Name && g.UserId == good.UserId && g.OnSale == 1) == 1)
            {
                return BadRequest("你已有一个同名商品在销售中");
            }
            try
            {
                _dbContext.Goods.Add(good);
                return Success("发布成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Error("发布失败");
            }
        }
    }
}