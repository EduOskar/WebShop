using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Api.Services;
using WebShop.Models.DTOs;
using OrderStatusType = WebShop.Models.DTOs.OrderStatusType;

namespace WebShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly IMapper _mapper;
    private readonly OrderWorkerService _orderWorkerService;

    public OrdersController(IOrderRepository orderRepository,
        IUserRepository userRepository,
        IOrderStatusRepository orderStatusRepository,
        IMapper mapper,
        OrderWorkerService orderWorkerService)
        
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _orderStatusRepository = orderStatusRepository;
        _mapper = mapper;
        _orderWorkerService = orderWorkerService;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<OrderDto>>> GetOrders()
    {
        var orders = _mapper.Map<List<OrderDto>>(await _orderRepository.GetOrders());

        if (orders != null)
        {
            return Ok(orders);
            
        }
        return BadRequest();
    }
      
    [HttpGet("Orders-from-user/{userId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersFromUser(int userId)
    {
        var orders = _mapper.Map<List<OrderDto>>(await _orderRepository.GetOrdersFromUser(userId));

        if (orders != null)
        {
            return Ok(orders);
        }

        return BadRequest();
    }

    [HttpGet("Last-Order-By-User/{userId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<OrderDto>> GetOrderFromUser(int userId)
    {
        var orders = _mapper.Map<OrderDto>(await _orderRepository.GetLastOrderFromUser(userId));

        if (orders != null)
        {
            return Ok(orders);
           
        }

        return BadRequest();
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

        var orderStatus = _mapper.Map<OrderStatusDto>(await _orderStatusRepository.GetOrderStatus(order.OrderStatusId));
        order.OrderStatus.CurrentStatus = orderStatus.CurrentStatus;
        order.OrderStatus.StatusDate = orderStatus.StatusDate;

        if (order != null)
        {
            return Ok(order);
        }

        return BadRequest();
    }

    [HttpPost]
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


        var status = await _orderStatusRepository.GetOrderStatus(orderCreate.OrderStatusId);

        var orderMap = _mapper.Map<Order>(orderCreate);
        orderMap.OrderStatus = status;

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

        if (!await _orderRepository.UpdateOrder(orderMap))
        {
            ModelState.AddModelError("", "Something went wrong while updating");
            return BadRequest();
        }

        return NoContent();
    }

    [HttpPut("OrderStatus/{orderId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateOrderStatus(int orderId, [FromBody]OrderStatusType newStatus)
    {
        var order = await _orderRepository.GetOrder(orderId);

        var updatedStatus = _mapper.Map<Entity.OrderStatusType>(newStatus);

        if (order != null)
        {
            order.OrderStatus.UpdateStatus(updatedStatus);
            order.OrderStatus.StatusDate = DateTime.UtcNow;

            if (await _orderRepository.UpdateOrder(order))
            {
                return NoContent();
            }
        }

        return BadRequest();
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

    [HttpPost("{orderId:int}/{userId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<bool>> AssignOrderToWorker(int orderId, int userId)
    {
        var response = await _orderWorkerService.AssignOrderToWarehouseWorker(orderId, userId);

        if (response)
        {
            return Ok(response);
        }

        return BadRequest();
    }
}
