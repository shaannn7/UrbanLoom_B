using System.ComponentModel.DataAnnotations;

namespace UrbanLoom_B.Dto.RegisterDto
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Mail { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
    }
}
