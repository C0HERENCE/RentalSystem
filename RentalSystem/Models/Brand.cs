using System;
using System.Collections.Generic;

#nullable disable

namespace RentalSystem.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Goods = new HashSet<Good>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Good> Goods { get; set; }
    }
}
