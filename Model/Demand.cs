namespace RentalServer.Model
{
    public class Demand
    {
        public int Id { get; set; }
        public int Checked { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        
        public User Publisher { get; set; }
        
        public int PublisherId { get; set; }
    }
}