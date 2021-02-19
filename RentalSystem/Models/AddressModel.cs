namespace RentalSystem.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Enabled { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Detail { get; set; }
    }
}