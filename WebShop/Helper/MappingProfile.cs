using AutoMapper;
using WebShop.Api.Entity;
using WebShop.Models.DTOs;
using WebShop.Models.DTOs.MailDtos;

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

        CreateMap<OrderItem, OrderItemDto>().ReverseMap()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product != null ? src.Product.Description : string.Empty))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

        CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();

        CreateMap<Product, ProductDto>().ReverseMap();

        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<Review, ReviewDto>().ReverseMap()

            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
        CreateMap<UserRole, UserRoleDto>().ReverseMap();

        CreateMap<OrderStatus, OrderStatusDto>().ReverseMap();

        CreateMap<Discount,  DiscountDto>().ReverseMap();

        CreateMap<DiscountUsage, DiscountUsageDto>().ReverseMap();

       CreateMap<ProductsDiscount, ProductDiscountsDto>().ReverseMap();

        CreateMap<SupportMail, SupportMailDto>().ReverseMap();
    }
}
