using System;
using System.Collections.Generic;

#nullable disable

namespace RentalSystem.Models
{
    public partial class OrderInfo
    {
        public int Id { get; set; }
        public int? GoodsId { get; set; }
        public int? OrderId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }

        public virtual Good Goods { get; set; }
        public virtual Order Order { get; set; }
    }
}
