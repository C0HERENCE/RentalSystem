using System;

namespace RentalServer.Model
{
    // 收藏记录
    public class FavouriteRecord
    {
        public int Id { get; set; }
        // 收藏者
        // public User User { get; set; } efcore的限制, 会有循环的外键依赖
        public int UserId { get; set; }
        // 收藏商品
        // public Product Product { get; set; }
        public int ProductId { get; set; }
        // 收藏时间
        public DateTime CreateTime { get; set; }
    }
}