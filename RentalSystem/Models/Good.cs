using System;
using System.Collections.Generic;
using Newtonsoft.Json;

#nullable disable

namespace RentalSystem.Models
{
    public partial class Good
    {
        public Good()
        {
            Carts = new HashSet<Cart>();
            OrderInfos = new HashSet<OrderInfo>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }
        public int? Enabled { get; set; }
        public int? OnSale { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string OriginalPrice { get; set; }
        public int? Stock { get; set; }
        public string DetailHtml { get; set; }

        [JsonIgnore]
        public virtual Brand Brand { get; set; }
        
        [JsonIgnore]
        public virtual Category Category { get; set; }
        
        [JsonIgnore]
        public virtual User User { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Cart> Carts { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
