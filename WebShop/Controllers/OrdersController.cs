using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public OrdersController(IOrderRepository orderRepository, 
        IUserRepository userRepository, 
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ICollection<OrderDto>>> GetOrders()
    {
        var orders = _mapper.Map<List<OrderDto>>(await _orderRepository.GetOrders());

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(orders);
    }

    [HttpGet("{orderId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<OrderDto>> GetOrder(int orderId)
    {
        if (!await _orderRepository.OrderExist(orderId))
        {
            return NotFound();
        }

        var order = _mapper.Map<OrderDto>(await _orderRepository.GetOrder(orderId));

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(order);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> CreateOrder([FromBody]OrderDto orderCreate)
    {
        if(orderCreate == null)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var userExist = await _userRepository.UserExist(orderCreate.UserId);

        if (!userExist)
        {
            ModelState.AddModelError("", $"User with Id {orderCreate.UserId} does not exist");
            return BadRequest(ModelState);
        }

        var orderMap = _mapper.Map<Order>(orderCreate);
        orderMap.User = await _userRepository.GetUser(orderCreate.UserId);

        if (!await _orderRepository.CreateOrder(orderMap))
        {
            ModelState.AddModelError("", "There was an error saving");
        }

        return CreatedAtAction("GetOrder", new {orderId = orderMap.Id}, orderMap);
    }

    [HttpPut("{orderId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateOrder(int orderId, [FromBody] OrderDto updateOrder)
    {
        if (updateOrder == null)
        {
            return BadRequest();
        }

        if (orderId != updateOrder.Id)
        {
            return BadRequest();
        }

        if (!await _orderRepository.OrderExist(orderId))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var userExist = await _userRepository.UserExist(updateOrder.UserId);

        if (!userExist)
        {
            ModelState.AddModelError("", $"User with Id {updateOrder.UserId} does not exist");
            return BadRequest(ModelState);
        }

        var orderMap = _mapper.Map<Order>(updateOrder);
        orderMap.User = await _userRepository.GetUser(updateOrder.UserId);

        if (!await _orderRepository.UpdateOrder(orderMap))
        {
            ModelState.AddModelError("", "Something went wrong while updating");
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{orderId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeleteOrder (int orderId)
    {
        if (!await _orderRepository.OrderExist(orderId))
        {
            return NotFound();
        }

        var orderDelete = await _orderRepository.GetOrder(orderId);

        if (!await _orderRepository.DeleteOrder(orderDelete))
        {
            return BadRequest();
        }

        return NoContent();
    }
}
