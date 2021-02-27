using System;
using System.Collections.Generic;

#nullable disable

namespace RentalSystem.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int? GoodsId { get; set; }
        public int? UserId { get; set; }
        public int? Enabled { get; set; }

        public virtual Good Goods { get; set; }
        public virtual User User { get; set; }
    }
}
