using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrbanLoom_B.DBcontext;
using UrbanLoom_B.Entity;
using UrbanLoom_B.Entity.Dto;
using UrbanLoom_B.Mapper;

namespace UrbanLoom_B.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _Mapper;
        private readonly DbContextClass _dbContextClass;
        private readonly IConfiguration _configuration;
        public UserService(IMapper mapper, IConfiguration configuration, DbContextClass dbContextClass)
        {
            _Mapper = mapper;
            _configuration = configuration;
            _dbContextClass = dbContextClass;
        }

        public async Task<bool> RegisterUser(RegisterDto registerDto)
        {
            var isUserExist = await _dbContextClass.Users_ul.FirstOrDefaultAsync(u => u.Mail == registerDto.Mail);
            if (isUserExist != null)
            {
                return false;
            }
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password, salt);
            registerDto.Password = hashPassword;

            var user = _Mapper.Map<User>(registerDto);
            _dbContextClass.Users_ul.Add(user);
            await _dbContextClass.SaveChangesAsync();

            return true;
        }

        public async Task<List<UserViewDto>> GetUsers()
        {
            var user = await _dbContextClass.Users_ul.ToListAsync();
            var users = _Mapper.Map<List<UserViewDto>>(user);
            return users;
        }

        public async Task<UserViewDto> GetUserByID(int id)
        {
            var user = await _dbContextClass.Users_ul.FirstOrDefaultAsync(u=>u.Id == id);
            var userr = _Mapper.Map<UserViewDto>(user);
            return userr;
        }

        public async Task<User> Login(LoginDto loginDto)
        {
            var ExstUser = await _dbContextClass.Users_ul.FirstOrDefaultAsync(u=>u.Mail==loginDto.Mail);
            return ExstUser;
        }

        public async Task<bool> BlockUser(int uID)
        {
            var user = await _dbContextClass.Users_ul.FirstOrDefaultAsync(i=>i.Id == uID);
            if (user == null)
            {
                return false;
            }
            user.isBlocked = true;
            await _dbContextClass.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnblockUser(int uID)
        {
            var user = await _dbContextClass.Users_ul.FirstOrDefaultAsync(i => i.Id == uID);
            if(user == null)
            {
                return false;
            }
            user.isBlocked = false;
            await _dbContextClass.SaveChangesAsync();
            return true;
        }
    }
}
