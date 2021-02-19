namespace RentalSystem.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IsAdmin { get; set; }
        public int Enabled { get; set; }
    }
}