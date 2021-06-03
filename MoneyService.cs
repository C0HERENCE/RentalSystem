using System;
using System.Linq;
using RentalServer.Data;
using RentalServer.Model;

namespace RentalServer
{
    // 用于记账
    public class MoneyService
    {
        private readonly RentalDbContext _dbContext;

        public MoneyService(RentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 用于记账和改变用户余额的函数, 返回0代表余额操作成功。 1代表余额不足，2表示用户不存在
        /// </summary>
        /// <param name="userId">传入额一个用户ID</param>
        /// <param name="money">传入改变的金额大小，正数代表收入，负数代表支出</param>
        /// <param name="note">一些特殊说明</param>
        public int ChangeMoney(int userId, float money, string note = "")
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return 2;
            if (user.Balance + money < 0)
                return 1;
            user.Balance += money; // 改余额
            _dbContext.Users.Update(user);
            _dbContext.Money.Add(new Money // 加金钱记录
            {
                After = user.Balance,
                Amount = money,
                Note = note,
                UserId = userId,
                CreateTime = DateTime.Now
            });
            _dbContext.SaveChanges();
            return 0;
        }
    }
}