using AutoMapper;
using WebShop.Api.Entity;
using WebShop.Models.DTOs;

namespace WebShop.Api.Helper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Cart, CartDto>().ReverseMap();
        CreateMap<CartItem, CartItemDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
        CreateMap<Product, ProductDto>();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Review, ReviewDto>().ReverseMap();
    }
}
