using System;
using System.Collections.Generic;

#nullable disable

namespace RentalSystem.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? GoodsId { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? DisplayStatus { get; set; }
        public string Content { get; set; }

        public virtual Good Goods { get; set; }
        public virtual User User { get; set; }
    }
}
