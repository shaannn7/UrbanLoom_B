using System.ComponentModel.DataAnnotations;

namespace UrbanLoom_B.Entity
{
    public class Cart
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        public virtual User user { get; set; }
        public virtual List<CartItem> cartitem { get; set; }
    }
}
