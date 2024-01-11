using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CartOrderTransfersController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public CartOrderTransfersController(IProductRepository productRepository,
        UserManager<User> userManager,
        IUserRepository userRepository,
        ICartRepository cartRepository,
        ICartItemRepository cartItemRepository,
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _userManager = userManager;
        _userRepository = userRepository;
        _cartRepository = cartRepository;
        _cartItemRepository = cartItemRepository;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
    }

    [HttpGet("{userId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<bool>> CartOrderTransfer(int userId)
    {
        decimal priceCheck = 0;

        string userIdentity = userId.ToString();

        var userManager = await _userManager.FindByIdAsync(userIdentity);

        var user = await _userRepository.GetUser(userManager!.Id);

        if (! await _userRepository.UserExist(userId))
        {
            return NotFound(false);
        }

        var usersCart = await _cartRepository.GetCartByUser(user.Id);

        if (! await _cartRepository.CartExist(usersCart.Id))
        {
            return NotFound(false);
        }

        if (!user.Id.Equals(usersCart.UserId))
        {
            return BadRequest(false);
        }

        var cartItems = await _cartItemRepository.GetCartItems(userId);

        var cartItemsCheck = cartItems.Where(ci => ci.CartId == usersCart.Id);

        if (!ModelState.IsValid)
        {
            return BadRequest(false);
        }

        if (cartItemsCheck.IsNullOrEmpty())
        {
            return BadRequest(false);
        }

        var products = await _productRepository.GetProducts();

        if (products.IsNullOrEmpty())
        {
            return NotFound(false);
        }

        foreach (var cartItem in cartItems)
        {
            priceCheck += cartItem.Product!.Price * cartItem.Quantity;
        }

        if (user.Credit <= priceCheck)
        {
            return BadRequest(false);
        }

        user.Credit -= priceCheck;

        var orderCreate = new OrderDto
        {
            UserId = usersCart.UserId,
            PlacementTime = DateTime.Now,
        };

        var orderMap = _mapper.Map<Order>(orderCreate);
        orderMap.UserId = usersCart.UserId;

        await _orderRepository.CreateOrder(orderMap);

        foreach (var cartItem in cartItems)
        {
            var orderItems = new OrderItemDto
            {
            };

            var orderItemMap = _mapper.Map<OrderItem>(orderItems);
            orderItemMap.Order = await _orderRepository.GetOrder(orderMap.Id);
            orderItemMap.Product = await _productRepository.GetProduct(cartItem.ProductId);
            orderItemMap.Quantity = cartItem.Quantity;

            await _orderItemRepository.CreateOrderItem(orderItemMap);

            var productCondition = products.Where(p => p.Id == cartItem.ProductId);

            foreach (var item in productCondition)
            {
                item.Quantity -= cartItem.Quantity;

            }

            await _cartItemRepository.DeleteCartItem(cartItem);
        }

        return Ok();
       
    }
}
