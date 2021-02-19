namespace RentalSystem.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int Enabled { get; set; }
        public string Description { get; set; }
    }
}