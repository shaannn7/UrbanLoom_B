namespace UrbanLoom_B.Entity.Dto
{
    public class UserViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool isBlocked { get; set; }
        public string Role { get; set; }
    }
}
