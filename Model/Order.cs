using System;

namespace RentalServer.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime BackTime { get; set; }
        public DateTime RealBackTime { get; set; }
        public int Status { get; set; }
        public float Rate { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        // public User Customer { get; set; }
        public int CustomerId { get; set; }
        public int Days { get; set; }

        public float ExtraMoney { get; set; }
    }
}