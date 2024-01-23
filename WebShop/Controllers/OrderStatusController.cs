using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderStatusController : ControllerBase
{
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly IMapper _mapper;

    public OrderStatusController(IOrderStatusRepository orderStatusRepository, IMapper mapper)
    {
        _orderStatusRepository = orderStatusRepository;
        _mapper = mapper;
    }

    [HttpGet("{orderStatusId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<OrderStatusDto>> GetOrderStatus(int orderStatusId)
    {
        var orderStatus = await _orderStatusRepository.GetOrderStatus(orderStatusId);

        if (orderStatus != null)
        {
            return Ok(orderStatus);
        }

        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<OrderStatusDto>> CreateOrderStatus([FromBody] OrderStatusDto orderStatus)
    {
        if (orderStatus == null)
        {
            return BadRequest();
        }

        var statusMap = _mapper.Map<OrderStatus>(orderStatus);

        if (!await _orderStatusRepository.CreateOrderStatus(statusMap))
        {
            return BadRequest();
        }

        return CreatedAtAction("GetOrderStatus", new { orderStatusId = statusMap.Id }, statusMap);
    }

    [HttpPut("{orderStatusId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateOrderStatus(int orderStatusId, [FromBody] OrderStatusDto orderStatus)
    {
        if (orderStatusId == orderStatus.Id)
        {
            var orderStatusUpdate = _mapper.Map<OrderStatus>(await _orderStatusRepository.GetOrderStatus(orderStatusId));

            if (orderStatusUpdate != null)
            {
                await _orderStatusRepository.UpdateOrderStatus(orderStatusUpdate);
            }

            return NoContent();
        }

        return BadRequest();
    }
}
