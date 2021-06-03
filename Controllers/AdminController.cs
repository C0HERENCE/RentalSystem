using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalServer.Data;
using RentalServer.Model;

namespace RentalServer.Controllers
{
    [ApiController]
    [Route("/api/admin")]
    public class AdminController : Controller
    {
        private readonly RentalDbContext _dbContext;

        public AdminController(RentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private bool ValidAdmin()
        {
            var id = HttpContext.Session.GetInt32("id");
            if (!id.HasValue)
                return false;
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            return user is {Enabled: true, IsAdmin: true};
        }

        // 获取学生信息
        [HttpGet]
        [Route("students")]
        public CommonResult<List<Student>> GetAllStudents()
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！", new List<Student>());
            return CommonResult.Success(_dbContext.Students
                .Include(s => s.Class)
                .ToList());
        }

        // 更新一个学生
        [HttpPost]
        [Route("updatestudents")]
        public CommonResult<string> UpdateStudentById(int id, string name, string schoolNum, string idCard, int classId)
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！");
            var firstOrDefault = _dbContext.Students.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault == null)
                return CommonResult.BadRequest("不存在");
            firstOrDefault.Name = name;
            firstOrDefault.SchoolNum = schoolNum;
            firstOrDefault.IdCard = idCard;
            firstOrDefault.ClassId = classId;
            _dbContext.Update(firstOrDefault);
            _dbContext.SaveChanges();
            return CommonResult.Success("修改成功");
        }


        // 获取用户信息
        [HttpGet]
        [Route("users")]
        public CommonResult<List<User>> GetAllUsers()
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！", new List<User>());
            return CommonResult.Success(_dbContext.Users
                .Include(u => u.Student)
                .Where(u => !u.IsAdmin)
                .ToList());
        }

        //更新用户
        [HttpPost]
        [Route("updateusers")]
        public CommonResult<string> UpdateUserById(int id, bool enabled)
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！");
            var firstOrDefault = _dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault == null)
                return CommonResult.BadRequest("不存在");
            firstOrDefault.Enabled = enabled;
            _dbContext.Update(firstOrDefault);
            _dbContext.SaveChanges();
            return CommonResult.Success("修改成功");
        }

        // 获取班级信息
        [HttpGet]
        [Route("classes")]
        public CommonResult<List<Class>> GetClasses()
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！", new List<Class>());
            return CommonResult.Success(_dbContext.Classes.ToList());
        }

        //添加班级
        [HttpPost]
        [Route("addclasses")]
        public CommonResult<string> AddClasses(string grade, string major)
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！");
            var @class = new Class
            {
                Grade = grade,
                Major = major
            };
            _dbContext.Add(@class);
            _dbContext.SaveChanges();
            return CommonResult.Success("添加成功");
        }

        // 获取订单信息
        [HttpGet]
        [Route("orders")]
        public CommonResult<List<Order>> GetOrders()
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！", new List<Order>());
            return CommonResult.Success(_dbContext.Orders
                .Include(o => o.Product)
                .ThenInclude(p => p.Publisher)
                .ThenInclude(x => x.Student)
                .ThenInclude(s => s.Class)
                .ToList());
        }

        //更新订单
        [HttpPost]
        [Route("updateorders")]
        public CommonResult<string> UpdateOrderById(int id, int status)
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！");
            var firstOrDefault = _dbContext.Orders.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault == null)
                return CommonResult.BadRequest("不存在");
            firstOrDefault.Status = status;
            _dbContext.Update(firstOrDefault);
            _dbContext.SaveChanges();
            return CommonResult.Success("修改成功");
        }

        // 获取需求信息
        [HttpGet]
        [Route("demands")]
        public CommonResult<List<Demand>> GetDemands()
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！", new List<Demand>());
            return CommonResult.Success(_dbContext.Demands
                .Include(p => p.Publisher)
                .ThenInclude(u => u.Student)
                .ThenInclude(s => s.Class).ToList());
        }

        //更新需求
        [HttpPost]
        [Route("demands")]
        public CommonResult<string> UpdateDemandById(int id, int Checked, bool enabled)
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！");
            var firstOrDefault = _dbContext.Demands.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault == null)
                return CommonResult.BadRequest("不存在");
            firstOrDefault.Checked = Checked;
            firstOrDefault.Enabled = enabled;
            _dbContext.Update(firstOrDefault);
            _dbContext.SaveChanges();
            return CommonResult.Success("修改成功");
        }

        // 获取分类信息
        [HttpGet]
        [Route("categories")]
        public CommonResult<List<Category>> GetCategories()
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！", new List<Category>());
            return CommonResult.Success(_dbContext.Categories.ToList());
        }

        //添加分类
        [HttpPost]
        [Route("addcategories")]
        public CommonResult<string> AddCategories(string name)
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！");
            var category = new Category
            {
                Name = name,
                Enabled = true
            };
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return CommonResult.Success("添加成功");
        }

        [HttpPost]
        [Route("updateCategory")]
        public CommonResult<string> UpdateCategoryById(int id, string newName, bool enabled)
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！");
            var firstOrDefault = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (firstOrDefault == null)
                return CommonResult.BadRequest("该分类不存在");
            firstOrDefault.Name = newName;
            firstOrDefault.Enabled = enabled;
            _dbContext.Update(firstOrDefault);
            _dbContext.SaveChanges();
            return CommonResult.Success("修改成功");
        }

        // 获取产品信息
        [HttpGet]
        [Route("products")]
        public CommonResult<List<Product>> GetProducts()
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！", new List<Product>());
            var products = _dbContext.Products.Include(p => p.Category)
                .Include(p => p.Publisher)
                .ThenInclude(p => p.Student)
                .ThenInclude(s => s.Class)
                .ToList();
            foreach (var product in products)
            {
                product.Category = null;
                product.Keywords ??= "";
            }

            return CommonResult.Success(products);
        }

        //更新产品
        [HttpPost]
        [Route("updateProduct")]
        public CommonResult<string> UpdateProductById(int id, int Checked, int enabled, int categoryId, string keywords)
        {
            if (!ValidAdmin())
                return CommonResult.BadRequest("没有管理权限！");
            var firstOrDefault = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault == null)
                return CommonResult.BadRequest("不存在");
            firstOrDefault.Checked = Checked;
            firstOrDefault.Enabled = enabled;
            firstOrDefault.CategoryId = categoryId;
            firstOrDefault.Keywords = keywords ?? "";
            _dbContext.Update(firstOrDefault);
            _dbContext.SaveChanges();
            return CommonResult.Success("修改成功");
        }
    }
}