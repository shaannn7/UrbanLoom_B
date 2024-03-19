using UrbanLoom_B.Dto.CartDtos;

namespace UrbanLoom_B.Dto.OrderDto
{
    public class OrderAdminDetailViewDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerCity { get; set; }
        public string HomeAddress { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string TransactionId { get; set; }

        public List<CartViewDto> OrderProducts { get; set; }

    }
}
