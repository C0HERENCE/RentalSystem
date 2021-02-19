using System;

namespace RentalSystem.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
        public int OrderStatus { get; set; }
        public int PaymentStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CommitTime { get; set; }
        public DateTime PayTime { get; set; }
        public DateTime ConfirmTime { get; set; }
        public string Note { get; set; }
    }
}