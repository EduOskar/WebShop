using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CartsController(ICartRepository cartRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<CartDto>>> GetCarts()
    {
        var carts = _mapper.Map<List<CartDto>>(await _cartRepository.GetCarts());

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(carts);
    }

    [HttpGet("{cartId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<CartDto>> GetCart(int cartId)
    {

        if (!await _cartRepository.CartExist(cartId))
        {
            return NotFound();
        }

        var cart = _mapper.Map<CartDto>(await _cartRepository.GetCart(cartId));

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(cart);
    }

    [HttpGet("Get-Cart-By-Users/{userId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<CartDto>> GetCartByUser(int userId)
    {

        if (!await _cartRepository.CartExist(userId))
        {
            return NotFound();
        }

        var cart = _mapper.Map<CartDto>(await _cartRepository.GetCart(userId));

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(cart);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> CreateCart([FromBody]CartDto cartCreate)
    {
        if (cartCreate == null)
        {
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userExist = await _userRepository.UserExist(cartCreate.UserId);

        if (!userExist)
        {
            ModelState.AddModelError("", $"User with Id {cartCreate.UserId} does not exist");
            return BadRequest(ModelState);
        }

        var cartMap = _mapper.Map<Cart>(cartCreate);
        cartMap.User = await _userRepository.GetUser(cartCreate.UserId);

        if (! await _cartRepository.CreateCart(cartMap))
        {
            ModelState.AddModelError("", "There was an error creating your Cart");
            return BadRequest(ModelState);
        }

        return CreatedAtAction(nameof(GetCart), new { cartId = cartCreate.Id }, cartCreate);
    }

    [HttpPut("{cartId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateCart(int cartId, [FromBody]CartDto cartUpdate)
    {

        if (cartUpdate == null)
        {
            return BadRequest();
        }

        if (cartId != cartUpdate.Id)
        {
            return BadRequest();
        }

        if (!await _cartRepository.CartExist(cartId))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cartMap = _mapper.Map<Cart>(cartUpdate);

        if (! await _cartRepository.UpdateCart(cartMap))
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{cartId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeleteCart(int cartId)
    {
        if (! await _cartRepository.CartExist(cartId))
        {
            return NotFound();
        }

        var cartDelete = await _cartRepository.GetCart(cartId);

        if (! await _cartRepository.DeleteCart(cartDelete))
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }
}
