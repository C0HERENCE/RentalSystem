namespace RentalSystem.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int GoodsId { get; set; }
        public int UserId { get; set; }
        public int Enabled { get; set; }
    }
}