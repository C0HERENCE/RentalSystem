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

        public GoodsController(RentalSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public IActionResult GetByCategoryId([FromQuery]int categoryId, [FromQuery]int page = 0, [FromQuery]int limit = 20)
        {
            if (limit > 50 || page < 1)
                return BadRequest("传输数据量过大");
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
                return NotFound("分类不存在");
            var cid = categoryId;
            return Success("ok",
                new
                {
                    data = _dbContext.Goods.Where(g => g.CategoryId == cid)
                        .OrderBy(g => g.Id)
                        .Skip((page - 1) * limit)
                        .Take(limit).ToList(),
                    total = _dbContext.Goods.Count(g => g.OnSale == 1 && g.Enabled == 1 && g.CategoryId == cid)
                });
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetByUserId()
        {
            var id = JwtService.GetUserId(HttpContext);
            return Success("ok",
                _dbContext.Goods.Where(g => g.UserId == id).ToList());
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
            var userId = JwtService.GetUserId(HttpContext);
            var good = new Good
            {
                UserId =  userId,
                BrandId =  goodsRequest.brand_id,
                CategoryId = goodsRequest.category_id,
                Name = goodsRequest.name,
                Enabled = 1,
                OnSale = 1, // TODO 管理员审核功能
                Price = goodsRequest.price,
                Pic = goodsRequest.pic,
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
                _dbContext.SaveChanges();
                return Success("发布成功", good.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Error("发布失败");
            }
        }
    }
}