using UrbanLoom_B.Entity.Dto;

namespace UrbanLoom_B.Services.WhishListService
{
    public interface IWhishList
    {
        Task<List<WhishListViewDto>> GetWhishList(string token);
        Task<bool> AddToWhishList(string Token, int ProductID);
        Task RemoveWhishList(string Token , int productID);
    }
}
