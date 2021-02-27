using System;
using System.Collections.Generic;

#nullable disable

namespace RentalSystem.Models
{
    public partial class User
    {
        public User()
        {
            Addresses = new HashSet<Address>();
            Carts = new HashSet<Cart>();
            Goods = new HashSet<Good>();
            OrderCustomers = new HashSet<Order>();
            OrderOperations = new HashSet<OrderOperation>();
            OrderSellers = new HashSet<Order>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? IsAdmin { get; set; }
        public int? Enabled { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Good> Goods { get; set; }
        public virtual ICollection<Order> OrderCustomers { get; set; }
        public virtual ICollection<OrderOperation> OrderOperations { get; set; }
        public virtual ICollection<Order> OrderSellers { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
