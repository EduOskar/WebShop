using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CartOrderTransfersController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;

    public CartOrderTransfersController(IProductRepository productRepository,
        IUserRepository userRepository,
        ICartItemRepository cartItemRepository,
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
        _cartItemRepository = cartItemRepository;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
    }

    //[HttpPost]
    //public async Task<bool> CartOrderTransfer()
    //{

    //}
}
