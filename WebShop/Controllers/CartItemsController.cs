using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CartItemsController : ControllerBase
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CartItemsController(ICartItemRepository cartItemRepository,
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _cartItemRepository = cartItemRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet("GetUsersCartItems/{userId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems(int userId)
    {
        var cartItems = _mapper.Map<List<CartItemDto>>(await _cartItemRepository.GetCartItems(userId));

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(cartItems);
    }

    [HttpGet("{cartItemId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<CartItem>> GetCartItem(int cartItemId)
    {
        if (!await _cartItemRepository.CartItemExist(cartItemId))
        {
            return NotFound();
        }

        var cartItem = _mapper.Map<CartItemDto>(await _cartItemRepository.GetCartItem(cartItemId));

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(cartItem);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> CreateCartItem([FromBody] CartItemDto cartItemCreate)
    {
        //if (cartItemCreate == null)
        //{
        //    return BadRequest();
        //}
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var cartExist = await _cartRepository.CartExist(cartItemCreate.CartId);

        if (!cartExist)
        {
            ModelState.AddModelError("", $"Cart with Id {cartItemCreate.CartId} does not exist");
            return BadRequest(ModelState);
        }

        var productExist = await _productRepository.ProductExist(cartItemCreate.ProductId);

        if (!productExist)
        {
            ModelState.AddModelError("", $"Product with Id {cartItemCreate.ProductId} does not exist");
            return BadRequest(ModelState);
        }

        var orderItemMap = _mapper.Map<CartItem>(cartItemCreate);
        orderItemMap.Cart = await _cartRepository.GetCart(cartItemCreate.CartId);
        orderItemMap.Product = await _productRepository.GetProduct(cartItemCreate.ProductId);

        if (!await _cartItemRepository.CreateCartItem(orderItemMap))
        {
            ModelState.AddModelError("", "There was an error saving");
            return BadRequest(ModelState);
        }

        return CreatedAtAction("GetCartItem", new { cartItemId = cartItemCreate.Id }, cartItemCreate);
    }

    [HttpPut("{cartItemId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateCartItem(int cartItemId, [FromBody] CartItemDto updateCartItem)
    {
        if (updateCartItem == null)
        {
            return BadRequest();
        }

        if (cartItemId != updateCartItem.Id)
        {
            return BadRequest();
        }

        if (!await _cartItemRepository.CartItemExist(cartItemId))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var cartExist = await _cartRepository.CartExist(updateCartItem.CartId);

        if (!cartExist)
        {
            ModelState.AddModelError("", $"Cart with Id {updateCartItem.CartId} does not exist");
            return BadRequest(ModelState);
        }

        var productExist = await _productRepository.ProductExist(updateCartItem.ProductId);

        if (!productExist)
        {
            ModelState.AddModelError("", $"Product with Id {updateCartItem.ProductId} does not exist");
            return BadRequest(ModelState);
        }

        var cartItemMap = _mapper.Map<CartItem>(updateCartItem);
        cartItemMap.Cart = await _cartRepository.GetCart(updateCartItem.CartId);
        cartItemMap.Product = await _productRepository.GetProduct(updateCartItem.ProductId);

        if (!await _cartItemRepository.UpdateCartItem(cartItemMap))
        {
            ModelState.AddModelError("", "Something went wrong while updating");
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{cartItemId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeleteOrderItem(int cartItemId)
    {
        if (!await _cartItemRepository.CartItemExist(cartItemId))
        {
            return NotFound();
        }

        var cartItemDelete = await _cartItemRepository.GetCartItem(cartItemId);

        if (!await _cartItemRepository.DeleteCartItem(cartItemDelete))
        {
            return BadRequest();
        }

        return NoContent();
    }
}
