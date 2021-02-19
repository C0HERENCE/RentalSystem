namespace RentalSystem.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public int GoodsId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}