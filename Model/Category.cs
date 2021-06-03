using System.Collections.Generic;

namespace RentalServer.Model
{
    public class Category
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public bool Enabled { get; set; }
    }
}