using System;
using System.Collections.Generic;

#nullable disable

namespace RentalSystem.Models
{
    public partial class OrderReturn
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public string Content { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? Status { get; set; }

        public virtual Order Order { get; set; }
    }
}
