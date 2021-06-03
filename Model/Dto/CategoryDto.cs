using System.Collections.Generic;

namespace RentalServer.Model.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<IndexProductDto> Products { get; set; }
    }
}