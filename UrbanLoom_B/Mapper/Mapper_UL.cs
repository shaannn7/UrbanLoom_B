
using AutoMapper;
using UrbanLoom_B.Entity;
using UrbanLoom_B.Entity.Dto;

namespace UrbanLoom_B.Mapper
{
    public class Mapper_UL : Profile
    {
        public Mapper_UL() 
        {
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, UserViewDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductViewDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryViewDto>().ReverseMap();
        }
    }
}
