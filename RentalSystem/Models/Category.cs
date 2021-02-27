using System;
using System.Collections.Generic;
using Newtonsoft.Json;

#nullable disable

namespace RentalSystem.Models
{
    public partial class Category
    {
        public Category()
        {
            Goods = new HashSet<Good>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? Enabled { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Good> Goods { get; set; }
    }
}
