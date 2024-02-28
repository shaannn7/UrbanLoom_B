using UrbanLoom_B.Dto.OrderDto;

namespace UrbanLoom_B.Services.OrderService
{
    public interface IOrder
    {

        /// BUY FROM CART ///
        Task<bool> CreateOrderFromCart(string token, OrderRequestDto orderRequest);

        /// BUY FROM SHOP ///
        Task<bool> CreateOrderFromShop(string token , OrderRequestDto orderRequest , int productid);

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
