using System;
using System.Collections.Generic;

#nullable disable

namespace RentalSystem.Models
{
    public partial class Address
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? Enabled { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Detail { get; set; }

        public virtual User User { get; set; }
    }
}
