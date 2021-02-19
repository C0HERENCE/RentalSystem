using System;

namespace RentalSystem.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GoodsId { get; set; }
        public DateTime CreateTime { get; set; }
        public int DisplayStatus { get; set; }
        public string Content { get; set; }
    }
}