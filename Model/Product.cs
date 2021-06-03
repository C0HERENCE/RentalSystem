namespace RentalServer.Model
{
    public class Product
    {
        public int Id { get; set; } 
        public int Checked { get; set; }
        public int Enabled { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public User Publisher { get; set; }
        public int PublisherId { get; set; }
        public float Price { get; set; }
        public Category Category { get; set; }
        
        public float YaMoney { get; set; }
        public int CategoryId { get; set; }
        public string Keywords { get; set; }

        public string Cover { get; set; }
    }
}