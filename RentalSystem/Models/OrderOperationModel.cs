using System;

namespace RentalSystem.Models
{
    public class OrderOperationModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int OperatorUserId { get; set; }
        public string Note { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrderStatus { get; set; }
    }
}