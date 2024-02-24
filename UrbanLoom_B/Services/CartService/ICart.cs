using UrbanLoom_B.Entity.Dto;

namespace UrbanLoom_B.Services.CartService
{
    public interface ICart
    {
        Task<List<CartViewDto>> GetCartItems(string token);
        Task AddToCart(string token, int productId);
        Task QuantityINC(string token, int productId);
        Task QuantityDEC(string token, int productId);
        Task DeleteCart(string token, int productId);
    }
}
