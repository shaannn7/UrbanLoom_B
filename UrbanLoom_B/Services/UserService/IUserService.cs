using UrbanLoom_B.Entity;
using UrbanLoom_B.Entity.Dto;

namespace UrbanLoom_B.Services.UserService
{
    public interface IUserService
    {
        Task<bool> RegisterUser(RegisterDto registerDto);
        Task<List<UserViewDto>> GetUsers();
        Task<UserViewDto> GetUserByID(int id);
        Task<User> Login(LoginDto loginDto);
        Task<bool> BlockUser(int uID);
        Task<bool> UnblockUser(int uID);
    }
}
