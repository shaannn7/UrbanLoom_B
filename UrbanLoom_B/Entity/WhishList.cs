using System.ComponentModel.DataAnnotations;

namespace UrbanLoom_B.Entity
{
    public class WhishList
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual User User { get; set; }
        public virtual Product products { get; set; }
    }
}
