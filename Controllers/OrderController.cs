using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalServer.Data;
using RentalServer.Model;

namespace RentalServer.Controllers
{
    [ApiController]
    [Route("/api/order")]
    public class OrderController : Controller
    {
        private readonly RentalDbContext _dbContext;
        private readonly MoneyService _moneyService;

        public OrderController(RentalDbContext rentalDbContext, MoneyService moneyService)
        {
            _dbContext = rentalDbContext;
            _moneyService = moneyService;
        }

        
        /// <summary>
        /// 新订单
        /// </summary>
        [Route("new")]
        [HttpPost]
        public CommonResult<int> NewOrder([FromBody] dynamic form)
        {
            // form : Product、Session->Id、Order -> day
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效", -1);
            var userId = HttpContext.Session.GetInt32("id").Value;
            var productId = (int) form["productId"];
            var day = (int) form["day"];
            
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) 
                return CommonResult.BadRequest("该用户不存在", -1);
            var product = _dbContext.Products
                .Include(p => p.Publisher)
                .FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return CommonResult.BadRequest("该商品不存在", -1);
            if (product.Checked == 0 || product.Checked == 2)
                return CommonResult.BadRequest("该商品尚未通过审核", -1);
            if (product.Enabled == 0)
                return CommonResult.BadRequest("该商品已下架", -1);
            if (userId == product.PublisherId)
                return CommonResult.BadRequest("不能购买自己的产品", -1);
            
            var total = product.Price * day; // total： 商品总价
            var yaMoney = product.YaMoney; // yaMoney: 押金
            
            if (user.Balance < total+yaMoney)
                return CommonResult.BadRequest("余额不足", -1);

            _moneyService.ChangeMoney(userId, -(total+yaMoney), $"{product.Name}租金、押金"); // 扣除租金+押金
          
            var order = new Order
            {
                CreateTime = DateTime.Now,
                BackTime = DateTime.Now.AddDays(day),
                CustomerId = userId,
                ProductId = productId,
                Rate = 0,
                Status = 0,
                Days = day
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return CommonResult.Success($"订单创建成功\n\r已支付：{total + yaMoney}元\n\r当前余额：{user.Balance}元", order.Id);
        }

        /// <summary>
        /// 卖家确认订单 或 拒绝订单
        /// </summary>
        [Route("confirm")]
        [HttpPost]
        public CommonResult<string> ConfirmOrder([FromBody] dynamic form)
        {
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效");
            var userId = HttpContext.Session.GetInt32("id").Value;
            var orderId = (int) form["orderId"];
            var msg = (string) form["msg"];
            if (msg != "yes" && msg != "no")
                return CommonResult.BadRequest("参数错误");
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) 
                return CommonResult.BadRequest("该用户不存在");
            var order = _dbContext.Orders
                .Include(o => o.Product) // 确保Product被Include进来
                .FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                return CommonResult.BadRequest("订单不存在");

            if (msg == "yes")
            {
                _moneyService.ChangeMoney(userId, +( order.Product.Price * order.Days), $"{order.Product.Name}租金入账"); // 卖家租金入账
                order.Status = 1; // 修改订单状态
                order.Product.Enabled = 0; // 修改该商品在售状态
                _dbContext.Orders.Update(order); // 保存到数据库
                _dbContext.SaveChanges();
                return CommonResult.Success("确认订单成功");
            }

            order.Status = 3;
            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();
            return CommonResult.Success("拒绝订单成功");
        }

        /// <summary>
        /// 买家还 + 余额操作
        /// </summary>
        [HttpPost]
        [Route("back")]
        public CommonResult<string> BackOrder([FromBody] dynamic form)
        {
            // 根据form的订单得到订单对象
            var orderId = (int) form["orderId"];
            var order = _dbContext.Orders
                .Include(o => o.Product)
                .FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                return CommonResult.BadRequest("订单不存在");
            // 根据session中的id得到卖家对象
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效");
            var publisherId = HttpContext.Session.GetInt32("id").Value;
            var publisher = _dbContext.Users.FirstOrDefault(u => u.Id == publisherId);
            if (publisher == null) 
                return CommonResult.BadRequest("该用户不存在");
            
            // 判断order的商品发布者是否和session 中相等
            if (publisherId != order.Product.PublisherId)
                return CommonResult.BadRequest("需要本人操作");
            
            // 根据订单对象中的customerId得到买家对象
            var customerId = order.CustomerId;
            var customer = _dbContext.Users.FirstOrDefault(u => u.Id == customerId);
            if (customer == null) 
                return CommonResult.BadRequest("该买家不存在");
            
            // 根据form的yaMoney计算要还给买家的金额
            var yaMoney = (float) form["yaMoney"]; // 用户输入的50 = yaMoney
            if (yaMoney > order.Product.YaMoney)
                return CommonResult.BadRequest("押金扣除的金额不能超过押金总额");
            var backToCustomer = order.Product.YaMoney - yaMoney; // 80 - 100 = 30 = backToCustomer
            
            // 修改order的完成时间、状态、更新卖家、买家余额
            order.RealBackTime = DateTime.Now;
            order.Status = 2;
            // customer.Balance += backToCustomer;  这里多加了一遍！ 然后money service里又加了一边！
            // publisher.Balance += yaMoney;这里多加了一遍！ 然后money service里又加了一边！
            
            // 此处给customer和publisher还押金 customerId = pp, publisherId = cc
            _moneyService.ChangeMoney(customerId, backToCustomer, $"{order.Product.Name}押金返还"); // pp + 30
            _moneyService.ChangeMoney(publisherId, yaMoney, $"{order.Product.Name}押金补偿"); // cc + 50
            
            order.ExtraMoney = yaMoney;
            _dbContext.Users.Update(customer);
            _dbContext.Users.Update(publisher);
            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();
            return CommonResult.Success("订单已完成");
        }

        [HttpPost]
        [Route("rate")]
        public CommonResult<string> RateOrder(int orderId, float value)
        {
            var order = _dbContext.Orders.FirstOrDefault(x=>x.Id==orderId);
            if (order == null)
                return CommonResult.BadRequest("订单不存在");
            // 根据session中的id得到买家对象
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效");
            var customerId = HttpContext.Session.GetInt32("id").Value;
            
            // 判断order的商品发布者是否和session 中相等
            if (customerId != order.CustomerId)
                return CommonResult.BadRequest("需要本人操作");
            order.Rate = value;
            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();
            return CommonResult.Success("评价成功");
        }
    }
}