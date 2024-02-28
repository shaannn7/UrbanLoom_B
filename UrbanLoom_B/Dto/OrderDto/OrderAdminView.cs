namespace UrbanLoom_B.Dto.OrderDto
{
    public class OrderAdminView
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
