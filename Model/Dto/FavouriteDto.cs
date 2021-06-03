using System;

namespace RentalServer.Model.Dto
{
    public class FavouriteDto
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string Cover { get; set; }
    }
}