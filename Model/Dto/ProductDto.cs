namespace RentalServer.Model.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public PersonalInfoDto Publisher { get; set; }
        public float Price { get; set; }
        public string YaMoney { get; set; }
        public CategoryDto Category { get; set; }
        public string Keywords { get; set; }
        public string Cover { get; set; }
        public int Enabled { get; set; }

        public bool Favourited { get; set; }
        // 代表这个商品，是否已经被当前用户给收藏了。
    }
}