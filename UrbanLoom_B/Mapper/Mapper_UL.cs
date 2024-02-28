
using AutoMapper;
using UrbanLoom_B.Dto.CartDtos;
using UrbanLoom_B.Dto.CategoryDto;
using UrbanLoom_B.Dto.ProductDto;
using UrbanLoom_B.Dto.RegisterDto;
using UrbanLoom_B.Dto.WhishListDto;
using UrbanLoom_B.Entity;

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
            CreateMap<WhishList,WhishLIstDto>().ReverseMap();
            CreateMap<WhishList, WhishListViewDto>().ReverseMap();
            CreateMap<Cart,CartViewDto>().ReverseMap();
        }
    }
}
