using System.ComponentModel.DataAnnotations.Schema;

namespace UrbanLoom_B.Dto.OrderDto
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public double TotalAmount { get; set; }

        [NotMapped]
        public string OrderId { get; set; }

        [NotMapped]

        public string TransactionId { get; set; }
    }
}
