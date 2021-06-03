using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit;
using RentalServer.Data;
using RentalServer.Model;
using RentalServer.Model.Dto;

namespace RentalServer.Controllers
{
    [ApiController] // controller
    [Route("/api/user")] // route： 使这个网址对应到这个地方
    public class UserController : Controller
    {
        private readonly RentalDbContext _dbContext;
        private readonly MoneyService _moneyService;

        public UserController(RentalDbContext dbContext, MoneyService moneyService)
        {
            _dbContext = dbContext;
            _moneyService = moneyService;
        }

        #region 密码加密，解密

        public static byte[] StrToByteArray(string str)
        {
            Dictionary<string, byte> hexindex = new Dictionary<string, byte>();
            for (int i = 0; i <= 255; i++)
                hexindex.Add(i.ToString("X2"), (byte) i);

            List<byte> hexres = new List<byte>();
            for (int i = 0; i < str.Length; i += 2)
                hexres.Add(hexindex[str.Substring(i, 2)]);

            return hexres.ToArray();
        }

        static string Encrypt(string plainText)
        {
            byte[] encrypted;
            byte[] IV;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = new byte[]
                    {0x00, 0x01, 0x02, 0xFF, 0xCC, 0xDD, 0x12, 0x34, 0x33, 0x12, 0x02, 0xFF, 0xCC, 0xDD, 0x12, 0x34};

                aesAlg.GenerateIV();
                IV = aesAlg.IV;

                aesAlg.Mode = CipherMode.CBC;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption. 
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            var combinedIvCt = new byte[IV.Length + encrypted.Length];
            Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
            Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);

            // Return the encrypted bytes from the memory stream. 
            return BitConverter.ToString(combinedIvCt).Replace("-", "");
            ;
        }

        static string Decrypt(string password)
        {
            var cipherTextCombined = StrToByteArray(password);
            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an Aes object 
            // with the specified key and IV. 
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = new byte[]
                    {0x00, 0x01, 0x02, 0xFF, 0xCC, 0xDD, 0x12, 0x34, 0x33, 0x12, 0x02, 0xFF, 0xCC, 0xDD, 0x12, 0x34};


                byte[] IV = new byte[aesAlg.BlockSize / 8];
                byte[] cipherText = new byte[cipherTextCombined.Length - IV.Length];

                Array.Copy(cipherTextCombined, IV, IV.Length);
                Array.Copy(cipherTextCombined, IV.Length, cipherText, 0, cipherText.Length);

                aesAlg.IV = IV;

                aesAlg.Mode = CipherMode.CBC;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption. 
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        #endregion

        [HttpPost]
        [Route("sendMail")]
        public CommonResult<string> SendMail([FromQuery] string email)
        {
            var captcha = $"{new Random().Next(0, 999999):000000}";
            HttpContext.Session.SetString("captcha", captcha);
            using var client = new SmtpClient();
            client.Connect("smtp.qq.com", 587, false);
            client.Authenticate("xiepongpong@qq.com", "mdmpkbpvzcahdjbi");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("小羊租赁官方", "xiepongpong@qq.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "欢迎来到小羊租赁";
            message.Body = new TextPart("plain") {Text = "你的验证码为：" + captcha + "。该验证码仅用于身份验证，请勿泄露给他人使用。"};
            client.Send(message);
            client.Disconnect(true);
            return CommonResult.Success("发送成功");
        }

        [HttpPost] // HttpPost： 只有post请求，才会到这个地方
        [Route("register")] // route： 使这个网址对应到这个地方
        public CommonResult<string> Register([FromBody] dynamic dictionary)
        {
            var schoolNum = (string) dictionary["schoolNum"];
            var studentName = (string) dictionary["studentName"];
            var classId = (int) dictionary["classId"];
            var idCard = (string) dictionary["IdCard"];
            idCard = idCard.ToUpper();
            // 验证 schoolNum studentName classId IdCard 是否对应Student表中的信息
            var student = _dbContext.Students
                .FirstOrDefault(x => x.SchoolNum == schoolNum
                                     && x.Name == studentName
                                     && x.ClassId == classId
                                     && x.IdCard == idCard);
            if (student == null)
                return CommonResult.BadRequest("学生信息有误！");
            // 邮箱验证
            if (dictionary["captcha"] != HttpContext.Session.GetString("captcha"))
                return CommonResult.BadRequest("验证码错误");
            // 判断User表是否已经有该学生Id了
            var count = _dbContext.Users.Count(x => x.StudentId == student.Id);
            if (count == 1)
                return CommonResult.BadRequest("该学生已被注册");
            // 加入到User表中
            _dbContext.Users.Add(new User
            {
                Enabled = true,
                Tel = "",
                Description = "",
                Email = dictionary["email"],
                Password = Encrypt((string) dictionary["password"]),
                SchoolNum = dictionary["schoolNum"],
                StudentId = student.Id
            });
            _dbContext.SaveChanges();
            return CommonResult.Success("注册成功");
        }

        [HttpPost]
        [Route("login")]
        public CommonResult<Dictionary<string, object>> Login([FromBody] dynamic dictionary)
        {
            var schoolNum = (string) dictionary["schoolNum"];
            var passWord = (string) dictionary["password"];
            var user = _dbContext.Users
                .Include(x => x.Student)
                .FirstOrDefault(x =>
                    x.SchoolNum == schoolNum);
            if (user == null)
                return CommonResult.BadRequest("用户不存在", new Dictionary<string, object>());

            if (Decrypt(user.Password) != passWord)
                return CommonResult.BadRequest("密码错误", new Dictionary<string, object>());

            HttpContext.Session.SetInt32("id", user.Id);
            Dictionary<string, object> result = new Dictionary<string, object>
            {
                {"name", user.Student.Name},
                {"isAdmin", user.IsAdmin}
            };
            return CommonResult.Success("登录成功", result);
        }

        [HttpGet]
        [Route("profile")]
        public CommonResult<ProfileDto> GetProfile()
        {
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效", new ProfileDto());

            var id = HttpContext.Session.GetInt32("id").Value;
            // 查询用户
            var user = _dbContext.Users
                .Include(u => u.Student)
                .ThenInclude(s => s.Class)
                .FirstOrDefault(x => x.Id == id);
            if (user == null)
                return CommonResult.BadRequest("该用户不存在", new ProfileDto());

            var profileDto = new ProfileDto();
            profileDto.PersonalInfo = new PersonalInfoDto
            {
                Balance = user.Balance.ToString("F2") + "元",
                Description = user.Description,
                Email = user.Email,
                IdCard = user.Student.IdCard,
                SchoolNum = user.Student.SchoolNum,
                StudentName = user.Student.Name,
                Tel = user.Tel,
                Class = user.Student.Class.Grade + user.Student.Class.Major
            };
            var buyOrders = _dbContext.Orders
                .Include(o => o.Product)
                .Where(o => o.CustomerId == id)
                .ToList();
            foreach (var order in buyOrders)
            {
                var profileOrderDto = new ProfileOrderDto
                {
                    CreateTime = order.CreateTime,
                    Id = order.Id,
                    ProductName = order.Product.Name,
                    Cover = "/uploads/" + order.Product.Cover,
                    Status = order.Status,
                    Days = order.Days,
                    YaMoney = order.Product.YaMoney,
                    ExtraMoney = order.ExtraMoney,
                    ZuMoney = order.Days * order.Product.Price,
                    Price = order.Product.Price,
                    Rate = order.Rate
                };
                if (profileOrderDto.Status == 0 || profileOrderDto.Status == 1 || profileOrderDto.Status == 3)
                    profileOrderDto.TotalPrice = order.Days * order.Product.Price + order.Product.YaMoney;
                else if (profileOrderDto.Status == 2)
                    profileOrderDto.TotalPrice = order.Days * order.Product.Price + order.ExtraMoney;
                profileDto.BuyOrders.Add(profileOrderDto);
            }

            var sellOrders = _dbContext.Orders
                .Include(o => o.Product)
                .Where(o => o.Product.PublisherId == id)
                .ToList();
            foreach (var order in sellOrders)
            {
                var profileOrderDto = new ProfileOrderDto
                {
                    CreateTime = order.CreateTime,
                    Id = order.Id,
                    ProductName = order.Product.Name,
                    Cover = "/uploads/" + order.Product.Cover,
                    Status = order.Status,
                    Days = order.Days,
                    YaMoney = order.Product.YaMoney,
                    ExtraMoney = order.ExtraMoney,
                    ZuMoney = order.Days * order.Product.Price,
                    Price = order.Product.Price,
                    Rate = order.Rate
                };
                if (profileOrderDto.Status == 0 || profileOrderDto.Status == 1 || profileOrderDto.Status == 3)
                    profileOrderDto.TotalPrice = order.Days * order.Product.Price + order.Product.YaMoney;
                else if (profileOrderDto.Status == 2)
                    profileOrderDto.TotalPrice = order.Days * order.Product.Price + order.ExtraMoney;
                profileDto.SellOrders.Add(profileOrderDto);
            }

            var published = _dbContext.Products
                .Where(p => p.PublisherId == id)
                .ToList();
            foreach (var product in published)
            {
                profileDto.Published.Add(new ProfilePublishedDto
                {
                    Checked = product.Checked,
                    Description = product.Description,
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Cover = "/uploads/" + product.Cover,
                    Enabled = product.Enabled
                });
            }

            return CommonResult.Success(profileDto);
        }

        [HttpPost]
        [Route("money")]
        public CommonResult<float> AddMoney([FromBody] dynamic entity)
        {
            float money = entity.money;
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效", -1.0f);
            if (money >= 10000.0f && money <= 0.00f)
                return CommonResult.BadRequest("请输入10000以内的金额", -1.0f);

            var id = HttpContext.Session.GetInt32("id").Value;
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return CommonResult.BadRequest("该用户不存在", -1.0f);

            _moneyService.ChangeMoney(id, +(money), $"充值");
            return CommonResult.Success("充值成功，当前余额" + user.Balance.ToString("F2") + "元", user.Balance);
        }


        [HttpPost]
        [Route("modifyInfo")]
        public CommonResult<string> ModifyInfo([FromBody] dynamic form)
        {
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效");
            var userId = HttpContext.Session.GetInt32("id").Value;
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return CommonResult.BadRequest("该用户不存在");
            user.Tel = form["tel"];
            user.Description = form["description"];
            _dbContext.Update(user);
            _dbContext.SaveChanges();
            return CommonResult.Success("修改成功");
        }

        [HttpPost]
        [Route("modify")]
        public CommonResult<string> modify([FromBody] dynamic form)
        {
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效");
            var userId = HttpContext.Session.GetInt32("id").Value;
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return CommonResult.BadRequest("该用户不存在");

            if (Decrypt(user.Password) != (string) form["oldPassword"])
                return CommonResult.BadRequest("原密码错误");

            user.Password = Encrypt((string) form["newPassword"]);
            _dbContext.Update(user);
            _dbContext.SaveChanges();
            return CommonResult.Success("修改成功");
        }

        [HttpGet]
        [Route("favourite")]
        public CommonResult<List<FavouriteDto>> GetMyFavourite()
        {
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效", new List<FavouriteDto>());
            var userId = HttpContext.Session.GetInt32("id").Value;

            var favouriteRecords = _dbContext.FavouriteRecords
                .Where(r => r.UserId == userId)
                .ToList(); // 得到我的所有收藏记录， 然而，这个收藏记录，只有商品ID， 商品收藏时间，
            // 接下来去获取商品的详细信息。就只要想首页那样显示然后点一下能到详细信息就好了到detail我的猜你喜欢就是这样的
            // 接下来根据productId， 补充完整FavouriteDTO中的所需内容
            var favouriteDtos = new List<FavouriteDto>();
            foreach (var favouriteRecord in favouriteRecords)
            {
                var product = _dbContext.Products
                    .FirstOrDefault(p => p.Id == favouriteRecord.ProductId); // 从数据库中，拿到这个Product的所有信息
                if (product == null || product.Enabled == 0 || product.Checked != 1) // 如果没找到这个商品，或者已经下架或者未审核通过
                    continue;
                var favouriteDto = new FavouriteDto
                {
                    Id = favouriteRecord.Id,
                    CreateTime = favouriteRecord.CreateTime,
                    ProductId = favouriteRecord.ProductId,
                    ProductName = product.Name, // 只取我们需要的，放到FavoriteDTO中
                    Cover = product.Cover,
                };
                favouriteDtos.Add(favouriteDto);
            }

            return CommonResult.Success(favouriteDtos);
        }

        [HttpGet]
        [Route("moneyHistory")]
        public CommonResult<List<Money>> GetMoneyHistory(DateTime start, DateTime end)
        {
            if (!HttpContext.Session.GetInt32("id").HasValue)
                return CommonResult.BadRequest("登录状态已失效", new List<Money>());
            var userId = HttpContext.Session.GetInt32("id").Value;
            var monies = _dbContext.Money
                .Where(r => r.UserId == userId && r.CreateTime >= start.AddHours(8) && r.CreateTime <= end.AddHours(8))
                .ToList();
            return CommonResult.Success(monies);
        }
    }
}