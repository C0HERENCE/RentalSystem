using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalServer.Data;
using RentalServer.Model;
using RentalServer.Model.Dto;

namespace RentalServer.Controllers
{
    [ApiController]
    [Route("/api/product")]
    public class ProductController : Controller
    {
        private readonly RentalDbContext _dbContext;

        public ProductController(RentalDbContext rentalDbContext, IWebHostEnvironment environment)
        {
            _dbContext = rentalDbContext;
            _hostingEnvironment = environment;
        }

        private readonly IWebHostEnvironment _hostingEnvironment;

        [HttpPost]
        [Route("add")]
        public CommonResult<int> AddProduct([FromBody] dynamic dictionary)
        {
            var p = new Product
            {
                Name = (string) dictionary["name"],
                Details = (string) dictionary["details"],
                Description = (string) dictionary["description"],
                CategoryId = (int) dictionary["category"],
                Price = (int) dictionary["price"],
                Cover = (string) dictionary["cover"],
                Checked = 0,
                Enabled = 0,
                YaMoney = (float) dictionary["yaMoney"]
            };
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效", -1);
            p.PublisherId = HttpContext.Session.GetInt32("id").Value;

            _dbContext.Products.Add(p);
            _dbContext.SaveChanges();
            return CommonResult.Success("发布成功, 请等待审核。", p.Id);
        }

        [HttpGet]
        [Route("checked")]
        public CommonResult<int> GetChecked([FromQuery] int id)
        {
            var p = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            if (p == null)
                return CommonResult.BadRequest("商品不存在", -1);

            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效", -1); 
            if (p.PublisherId != HttpContext.Session.GetInt32("id").Value)
                return CommonResult.BadRequest("无权限", -1);
            return CommonResult.Success(p.Checked);
        }

        [HttpGet]
        [Route("all")]
        public CommonResult<List<CategoryDto>> GetAll()
        {
            var s = _dbContext.Categories
                .Include(x => x.Products)
                .ThenInclude(p => p.Publisher)
                .ToList();
            var result = new List<CategoryDto>();
            foreach (var category in s)
            {
                if (category.Enabled == false)
                    continue;
                var i = new CategoryDto
                {
                    Id = category.Id, Label = category.Name, Products = new List<IndexProductDto>()
                };
                foreach (var product in category.Products
                    .Where(product => product.Checked == 1 && product.Enabled == 1 && product.Publisher.Enabled))
                {
                    i.Products.Add(new IndexProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price.ToString("F2") + "元",
                        Cover = product.Cover
                    });
                }

                result.Add(i);
            }

            return CommonResult.Success("ok", result);
        }

        [HttpPost]
        [Route("cover")]
        public CommonResult<string> UploadCover([FromForm] AddDataModel model)
        {
            //限制图片的格式
            if (model.Cover.ContentType != "image/png" &&
                model.Cover.ContentType != "image/jpg" &&
                model.Cover.ContentType != "image/jpeg")
                return CommonResult.BadRequest("请上传jpg或png图片");
            //限制图片的大小
            if (model.Cover.Length > 2097152)
                return CommonResult.BadRequest("请上传小于2MB的图片");
            //给图片取一个唯一的名字
            var newFileName = DateTime.Now.Ticks + ".png";
            //拼接保存路径,_hostingEnvironment.WebRootPath是项目路径下的wwwroot
            var filePath = Path.Combine(
                Path.Combine(
                    _hostingEnvironment.WebRootPath, "uploads")
                , newFileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            model.Cover.CopyTo(fileStream);
            return CommonResult.Success("上传成功", newFileName);
        }

        public class AddDataModel
        {
            public IFormFile Cover { get; set; }
        }

        [HttpPost]
        [Route("like/{id}")]
        public CommonResult<String> LikeProduct([FromRoute] int id)
        {
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效");
            var userId = HttpContext.Session.GetInt32("id").Value;
            // 判断是否已经收藏过这个商品
            var count = _dbContext.FavouriteRecords.Count(r => r.ProductId == id && r.UserId == userId);
            if (count > 0)
                return CommonResult.BadRequest("你已收藏过该商品");
            // 加入一个收藏记录
            _dbContext.FavouriteRecords.Add(new FavouriteRecord
            {
                CreateTime = DateTime.Now,
                ProductId = id,
                UserId = userId
            });
            _dbContext.SaveChanges();
            return CommonResult.Success("收藏成功");
        }

        [HttpPost]
        [Route("dislike/{id}")]
        public CommonResult<string> DislikeProduct([FromRoute] int id)
        {
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效");
            var userId = HttpContext.Session.GetInt32("id").Value;
            // 判断是否已经收藏过这个商品
            var count = _dbContext.FavouriteRecords.Count(r => r.ProductId == id && r.UserId == userId);
            if (count == 0)
                return CommonResult.BadRequest("你还没有收藏过该商品");
            var favouriteRecords = _dbContext.FavouriteRecords
                .Where(r => r.UserId == userId && r.ProductId == id)
                .ToList(); // 获取所有这个用户的这个商品的收藏记录 （应该只有一条）
            _dbContext.FavouriteRecords.RemoveRange(favouriteRecords);
            _dbContext.SaveChanges();
            return CommonResult.Success("取消收藏成功");
        }

        [HttpGet]
        [Route("{id}")]
        public CommonResult<ProductDto> GetById([FromRoute] int id)
        {
            var product = _dbContext.Products
                .Include(p => p.Publisher)
                .ThenInclude(pub => pub.Student)
                .ThenInclude(stu => stu.Class)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return CommonResult.BadRequest("没有该商品", new ProductDto());
            if (product.Checked == 0 || product.Checked == 2) //0 待审核 1 审核通过 2 审核不通过
                return CommonResult.BadRequest("该商品尚未通过审核！", new ProductDto());

            var productDto = new ProductDto
            {
                Category = new CategoryDto
                {
                    Id = product.Category.Id,
                    Label = product.Category.Name,
                    Products = null
                },
                Cover = product.Cover,
                Description = product.Description,
                Details = product.Details,
                Id = product.Id,
                Keywords = product.Keywords,
                Name = product.Name,
                Enabled = product.Enabled,
                Price = product.Price,
                YaMoney = product.YaMoney.ToString("F2") + "元",
                Favourited = false,
                Publisher = new PersonalInfoDto
                {
                    Id = product.PublisherId,
                    Balance = "",
                    Class = product.Publisher.Student.Class.Grade + product.Publisher.Student.Class.Major,
                    Description = product.Publisher.Description,
                    Email = product.Publisher.Email,
                    IdCard = "",
                    SchoolNum = product.Publisher.SchoolNum,
                    StudentName = product.Publisher.Student.Name,
                    Tel = product.Publisher.Tel,
                }
            };
            
            // 要去数据库中，获取当前登录用户，是否收藏过该商品
            if (HttpContext.Session.GetInt32("id").HasValue) // 如果当前已经登录
            {
                var userId = HttpContext.Session.GetInt32("id").Value; // 如果有登录，这就是当前用户ID
                var count = _dbContext.FavouriteRecords
                    .Count(r => r.UserId == userId && r.ProductId == productDto.Id);
                if (count > 0)
                    productDto.Favourited = true;
            }
            
            return CommonResult.Success("OK", productDto);
        }

        [HttpPost]
        [Route("delete/{id}")]
        public CommonResult<string> DeleteProduct([FromRoute] int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return CommonResult.BadRequest("该商品不存在");

            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效");
            var userId = HttpContext.Session.GetInt32("id").Value;

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return CommonResult.BadRequest("你不存在");

            if (user.Id != product.PublisherId || user.IsAdmin)
                return CommonResult.BadRequest("无权限下架商品");

            product.Enabled = 0;
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return CommonResult.Success("下架成功");
        }

        [HttpGet]
        [Route("search")]
        // 根据关键词搜索商品
        public CommonResult<List<ProductDto>> SearchProducts(string keywords)
        {
            var products = _dbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Publisher)
                .ThenInclude(p => p.Student)
                .ThenInclude(s => s.Class)
                .Where(x =>
                    x.Name.Contains(keywords) || x.Category.Name.Contains(keywords) || x.Keywords.Contains(keywords)
                    || x.Details.Contains(keywords) || x.Description.Contains(keywords) ||
                    x.Publisher.Student.Name.Contains(keywords)) // Where中是keywords可能出现的所有位置
                .Where(p => p.Enabled == 1 && p.Checked == 1 && p.Category.Enabled && p.Publisher.Enabled)
                .ToList();
            var list = new List<ProductDto>();
            foreach (var product in products)
            {
                list.Add(new ProductDto
                {
                    Category = new CategoryDto
                    {
                        Id = product.Category.Id,
                        Label = product.Category.Name,
                        Products = null
                    },
                    Cover = product.Cover,
                    Description = product.Description,
                    Details = product.Details,
                    Id = product.Id,
                    Keywords = product.Keywords,
                    Name = product.Name,
                    Enabled = product.Enabled,
                    Price = product.Price,
                    YaMoney = product.YaMoney.ToString("F2") + "元",
                    Publisher = new PersonalInfoDto
                    {
                        Balance = "",
                        Class = product.Publisher.Student.Class.Grade + product.Publisher.Student.Class.Major,
                        Description = product.Publisher.Description,
                        Email = product.Publisher.Email,
                        IdCard = "",
                        SchoolNum = product.Publisher.SchoolNum,
                        StudentName = product.Publisher.Student.Name,
                        Tel = product.Publisher.Tel
                    }
                });
            }
            return CommonResult.Success(list);
        }

        public List<ProductDto> GetProductByKeywords(string keywords, int size)
        {
            var products = _dbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Publisher)
                .ThenInclude(p => p.Student)
                .ThenInclude(s => s.Class)
                .Where(p => p.Keywords.Contains(keywords))
                .Where(p => p.Enabled == 1 && p.Checked == 1 && p.Category.Enabled && p.Publisher.Enabled)
                .Take(size)
                .ToList();
            var list = new List<ProductDto>();
            foreach (var product in products)
            {
                list.Add(new ProductDto
                {
                    Category = new CategoryDto
                    {
                        Id = product.Category.Id,
                        Label = product.Category.Name,
                        Products = null
                    },
                    Cover = product.Cover,
                    Description = product.Description,
                    Details = product.Details,
                    Id = product.Id,
                    Keywords = product.Keywords,
                    Name = product.Name,
                    Enabled = product.Enabled,
                    Price = product.Price,
                    YaMoney = product.YaMoney.ToString("F2") + "元",
                    Publisher = new PersonalInfoDto
                    {
                        Balance = "",
                        Class = product.Publisher.Student.Class.Grade + product.Publisher.Student.Class.Major,
                        Description = product.Publisher.Description,
                        Email = product.Publisher.Email,
                        IdCard = "",
                        SchoolNum = product.Publisher.SchoolNum,
                        StudentName = product.Publisher.Student.Name,
                        Tel = product.Publisher.Tel
                    }
                });
            }
            return list;
        }

        [HttpGet]
        [Route("guess")]
        public CommonResult<List<ProductDto>> GuessYouLike()
        {
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效", new List<ProductDto>());

            var userId = HttpContext.Session.GetInt32("id").Value;
            
            // 获取所有成功订单
            var orders = _dbContext.Orders.Include(o => o.Product)
                .Where(o => o.Status == 2 && o.CustomerId == userId)
                .ToList();
            
            // 获取所有收藏记录
            var favouriteRecords = _dbContext.FavouriteRecords
                .Where(r => r.UserId == userId)
                .ToList();
            // 收藏记录和购买记录里所有的商品的标签，都会被保存到这个Dictionary中，key是商品的标签，value是标签出现次数。
            var tags = new Dictionary<string, int>();
            
            // 遍历所有订单里的东东，得到他们的标签，存储起来。
            foreach (var order in orders)
            {
                foreach (var tag in order.Product.Keywords.Split(','))
                {
                    if (tag.Trim() != string.Empty)
                    {
                        if (!tags.ContainsKey(tag))
                            tags.Add(tag, 0);
                        tags[tag]++;
                    }
                }
            }
            
            // 遍历所有收藏记录里的东东，得到他们的标签，存储起来
            foreach (var favouriteRecord in favouriteRecords)
            {
                var product = _dbContext.Products.FirstOrDefault(p => p.Id == favouriteRecord.ProductId);
                if (product == null) continue;
                foreach (var tag in product.Keywords.Split(','))
                {
                    if (tag.Trim() != string.Empty)
                    {
                        if (!tags.ContainsKey(tag))
                            tags.Add(tag, 0);
                        tags[tag]++;
                    }
                }
            }
            
            // 降序排列所有的标签
            var keyValuePairs = tags.OrderByDescending(t => t.Value);
            if (keyValuePairs.Count() < 2) // 如果用户的所有标签个数过少，则不进行猜你喜欢。
                return CommonResult.Success(new List<ProductDto>());
            
            var firstKeyword = keyValuePairs.FirstOrDefault(); // 出现最多次的关键词
            var secondKeyword = keyValuePairs.Skip(1).First(); // 出现第二多次的关键词

            var productsDto1 = GetProductByKeywords(firstKeyword.Key, 2); // 去根据这个标签得到2个相关商品
            var productsDto2 = GetProductByKeywords(secondKeyword.Key, 1); // 去根据这个标签得到1个相关商品
            
            var result = new List<ProductDto>();
            result.AddRange(productsDto1);
            result.AddRange(productsDto2);
            var productDtos = result.Distinct(new Cmp()).ToList();
            return CommonResult.Success(productDtos);
        }
    }

    public class Cmp : IEqualityComparer<ProductDto>
    {
        public bool Equals(ProductDto x, ProductDto y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id;
        }

        public int GetHashCode(ProductDto obj)
        {
            return obj.Id;
        }
    }
}