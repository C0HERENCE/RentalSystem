using System;
using System.Collections.Generic;

#nullable disable

namespace RentalSystem.Models
{
    public partial class OrderOperation
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? OperatorUserId { get; set; }
        public string Note { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? OrderStatus { get; set; }

        public virtual User OperatorUser { get; set; }
        public virtual Order Order { get; set; }
    }
}
