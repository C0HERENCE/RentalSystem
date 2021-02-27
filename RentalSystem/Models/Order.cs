using System;
using System.Collections.Generic;

#nullable disable

namespace RentalSystem.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderInfos = new HashSet<OrderInfo>();
            OrderOperations = new HashSet<OrderOperation>();
            OrderReturns = new HashSet<OrderReturn>();
        }

        public int Id { get; set; }
        public int? SellerId { get; set; }
        public int? CustomerId { get; set; }
        public int? AddressId { get; set; }
        public int? OrderStatus { get; set; }
        public int? PaymentStatus { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? CommitTime { get; set; }
        public DateTime? PayTime { get; set; }
        public DateTime? ConfirmTime { get; set; }
        public string Note { get; set; }

        public virtual User Customer { get; set; }
        public virtual User Seller { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
        public virtual ICollection<OrderOperation> OrderOperations { get; set; }
        public virtual ICollection<OrderReturn> OrderReturns { get; set; }
    }
}
