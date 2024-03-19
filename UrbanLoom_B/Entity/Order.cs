using System.ComponentModel.DataAnnotations;

namespace UrbanLoom_B.Entity
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int userId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        [Required]
        public string CustomerCity { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        [Required]
        public string OrderStatus { get; set; }
        [Required]
        public string TransactionId { get; set; }
        [Required]
        public string OrderString { get; set; }


        public User users { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
