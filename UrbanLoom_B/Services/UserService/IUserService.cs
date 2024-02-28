using UrbanLoom_B.Dto.LoginDto;
using UrbanLoom_B.Dto.RegisterDto;
using UrbanLoom_B.Entity;

namespace UrbanLoom_B.Services.UserService
{
    public interface IUserService
    {
        Task<bool> RegisterUser(RegisterDto registerDto);
        Task<User> Login(LoginDto loginDto);

        Task<List<UserViewDto>> GetUsers();
        Task<UserViewDto> GetUserByID(int id);
        
        Task<bool> BlockUser(int uID);
        Task<bool> UnblockUser(int uID);
    }
}
