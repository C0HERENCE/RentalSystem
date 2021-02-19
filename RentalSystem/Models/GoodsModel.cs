using System;

namespace RentalSystem.Models
{
    public class GoodsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }
        public int Enabled { get; set; }
        public int OnSale { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string OriginalPrice { get; set; }
        public int Stock { get; set; }
        public string DetailHtml { get; set; }
    }
}