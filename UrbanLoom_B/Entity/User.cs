using System.ComponentModel.DataAnnotations;

namespace UrbanLoom_B.Entity
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string Role { get; set; }
        public bool isBlocked { get; set; }

        public virtual Cart cart { get; set; }
        public virtual List<Order> order { get; set; }
        public virtual List<WhishList> whishlist { get; set; }

    }
}
