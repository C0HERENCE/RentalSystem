using System;

namespace RentalServer.Model
{
    public class Money
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public float Amount { get; set; }
        public string Note { get; set; }
        public float After { get; set; }

        public DateTime CreateTime { get; set; }
    }
}