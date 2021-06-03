namespace RentalServer.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SchoolNum { get; set; }
        public string IdCard { get; set; }
        public Class Class { get; set; }
        public int ClassId { get; set; }
    }
}