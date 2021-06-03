using System;
using System.Collections.Generic;

namespace RentalServer.Model.Dto
{
    public class ProfilePublishedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Checked { get; set; }
        public string Cover { get; set; }

        public int Enabled { get; set; }
    }

    public class ProfileOrderDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Cover { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreateTime { get; set; }
        public int Status { get; set; }
        public int Days { get; set; }

        public float YaMoney { get; set; }

        public float ExtraMoney { get; set; }

        public float ZuMoney { get; set; }

        public float Price { get; set; }

        public float Rate { get; set; }
    }
    
    public class ProfileDto
    {
        public PersonalInfoDto PersonalInfo { get; set; }
        // 我发布的
        public List<ProfilePublishedDto> Published { get; set; } = new List<ProfilePublishedDto>();
        // 我的订单
        public List<ProfileOrderDto> BuyOrders { get; set; } = new List<ProfileOrderDto>();
        
        public List<ProfileOrderDto> SellOrders { get; set; } = new List<ProfileOrderDto>();
        
        
        
    }
}