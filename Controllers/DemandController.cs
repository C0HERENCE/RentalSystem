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
    [Route("/api/demand")]
    public class DemandController : Controller
    {
        private readonly RentalDbContext _dbContext;

        public DemandController(RentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("add")]
        public CommonResult<int> AddDemand([FromBody] dynamic body)
        {
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效", -1);
            int id = HttpContext.Session.GetInt32("id").Value;
            string name = body["name"];
            string content = body["content"];
            var demand = new Demand
            {
                Checked = 0, // 默认审核未通过
                Enabled = false, // 默认不显示
                Content = content,
                Name = name,
                PublisherId = id
            };
            _dbContext.Demands.Add(demand);
            _dbContext.SaveChanges();
            return CommonResult.Success("添加成功", demand.Id);
        }
        
        
        [HttpGet]
        [Route("checked")]
        public CommonResult<int> GetChecked([FromQuery] int id)
        {
            var p = _dbContext.Demands.FirstOrDefault(x => x.Id == id);
            if (p==null)
                return CommonResult.BadRequest("需求不存在", -1);
            
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效", -1); 
            return CommonResult.Success(p.Checked);
        }

        [HttpGet]
        [Route("all")]
        // 获取所有需求,
        public CommonResult<List<Demand>> GetAllDemand()
        {
            return CommonResult.Success(_dbContext.Demands
                .Include(d => d.Publisher)
                .Where(x => x.Enabled && x.Checked == 1 && x.Publisher.Enabled)
                .ToList());
        }
    }
}