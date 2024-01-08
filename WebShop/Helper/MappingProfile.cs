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
        CreateMap<CartItem, CartItemQtyUpdateDto>().ReverseMap();
        CreateMap<CartItemDto, CartItemQtyUpdateDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Review, ReviewDto>().ReverseMap();
        CreateMap<UserRole, UserRoleDto>().ReverseMap();
    }
}
