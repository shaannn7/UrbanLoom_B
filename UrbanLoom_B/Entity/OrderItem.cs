using System.ComponentModel.DataAnnotations;

namespace UrbanLoom_B.Entity
{
    public class OrderItem
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual Order order { get; set; }
        public virtual Product products { get; set; }
    }
}
