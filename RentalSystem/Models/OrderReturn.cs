using System;

namespace RentalSystem.Models
{
    public class OrderReturn
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int Status { get; set; }
    }
}