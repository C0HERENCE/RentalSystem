using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalServer.Model
{
    public class User
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string SchoolNum { get; set; }
        public string Password { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Tel { get; set; }
        
        [Column(TypeName = "float(5, 5)")]
        public float Balance { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}