using UrbanLoom_B.Entity.Dto;

namespace UrbanLoom_B.Services.OrderService
{
    public interface IOrder
    {

        ///CART///
        Task<bool> CreateOrderFromCart(string token, OrderRequestDto orderRequest);

        ///SHOP///
        Task<bool> CreateOrderFromShop(string token , OrderRequestDto orderRequest , int productid);

        ///ADMIN///
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
