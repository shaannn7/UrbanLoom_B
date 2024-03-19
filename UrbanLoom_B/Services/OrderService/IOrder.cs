using UrbanLoom_B.Dto.OrderDto;

namespace UrbanLoom_B.Services.OrderService
{
    public interface IOrder
    {

        Task<string> OrderCreate(long price);
        public bool Payment(RazorpayDto razorpay);

        /// BUY FROM CART ///
        Task<bool> CreateOrderFromCart(string token, OrderRequestDto orderRequest);

        Task<List<OrderViewDto>> GetOrderDtails(string token);

        /// ADMIN ///
        Task<List<OrderAdminView>> AdminGetAllOrders();
        Task<OrderAdminDetailViewDto> GetDetailedOrderDetailsByOrderID(int orderid);
        Task<List<OrderViewDto>> OrderDetailsByUserId(int userId);
        Task<int> GetTotalOrders();
        Task<decimal> GetTotalRevenue();
        Task<int> GetTodaysTotalOrders();
        Task<decimal> GetTodaysTotalRevenue();
        Task<bool> UpdateOrderStatus(int orderID , OrderUpdateDto orderUpdate);
    }
}
