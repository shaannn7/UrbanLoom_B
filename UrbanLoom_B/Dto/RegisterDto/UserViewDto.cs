namespace UrbanLoom_B.Dto.RegisterDto
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
