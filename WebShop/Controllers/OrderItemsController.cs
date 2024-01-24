using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderItemsController(IOrderItemRepository orderItemRepository,
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _orderItemRepository = orderItemRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<OrderItem>>> GetOrderItems()
    {
        var orderItems = _mapper.Map<List<OrderItemDto>>(await _orderItemRepository.GetOrderItems());

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(orderItems);
    }

    [HttpGet("Order-Items-From-User/{orderId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<OrderItem>>> GetOrderItemsFromUser(int orderId)
    {

        var orderItems = _mapper.Map<List<OrderItemDto>>(await _orderItemRepository.GetOrderItemsFromOrder(orderId));

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(orderItems);
    }

    [HttpGet("{orderItemId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<OrderItemDto>> GetOrderItem(int orderItemId)
    {
        if (!await _orderItemRepository.OrderItemExist(orderItemId))
        {
            return NotFound();
        }

        var orderItem = _mapper.Map<OrderItemDto>(await _orderItemRepository.GetOrderItem(orderItemId));

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(orderItem);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> CreateOrderItem([FromBody] OrderItemDto orderItemCreate)
    {
        if (orderItemCreate == null)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var orderExist = await _orderRepository.OrderExist(orderItemCreate.OrderId);

        if (!orderExist)
        {
            ModelState.AddModelError("", $"Order with Id {orderItemCreate.OrderId} does not exist");
            return BadRequest(ModelState);
        }

        var productExist = await _productRepository.ProductExist(orderItemCreate.ProductId);

        if (!productExist)
        {
            ModelState.AddModelError("", $"User with Id {orderItemCreate.ProductId} does not exist");
            return BadRequest(ModelState);
        }

        var orderItemMap = _mapper.Map<OrderItem>(orderItemCreate);
        orderItemMap.Order = await _orderRepository.GetOrder(orderItemCreate.OrderId);
        orderItemMap.Product = await _productRepository.GetProduct(orderItemCreate.ProductId);

        if (!await _orderItemRepository.CreateOrderItem(orderItemMap))
        {
            ModelState.AddModelError("", "There was an error saving");
            return BadRequest(ModelState);
        }

        return CreatedAtAction("GetOrderItem", new { orderItemId = orderItemMap.Id }, orderItemMap);
    }

    [HttpPut("{orderItemId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateOrderItem(int orderItemId, [FromBody] OrderItemDto updateOrderItem)
    {
        if (updateOrderItem == null)
        {
            return BadRequest();
        }

        if (orderItemId != updateOrderItem.Id)
        {
            return BadRequest();
        }

        if (!await _orderItemRepository.OrderItemExist(orderItemId))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var orderExist = await _orderRepository.OrderExist(updateOrderItem.OrderId);

        if (!orderExist)
        {
            ModelState.AddModelError("", $"User with Id {updateOrderItem.OrderId} does not exist");
            return BadRequest(ModelState);
        }

        var productExist = await _productRepository.ProductExist(updateOrderItem.ProductId);

        if (!productExist)
        {
            ModelState.AddModelError("", $"User with Id {updateOrderItem.ProductId} does not exist");
            return BadRequest(ModelState);
        }

        var orderItemMap = _mapper.Map<OrderItem>(updateOrderItem);
        orderItemMap.Order = await _orderRepository.GetOrder(updateOrderItem.OrderId);
        orderItemMap.Product = await _productRepository.GetProduct(updateOrderItem.ProductId);

        if (!await _orderItemRepository.UpdateOrderItem(orderItemMap))
        {
            ModelState.AddModelError("", "Something went wrong while updating");
            return BadRequest();
        }

        return NoContent();
    }

    [HttpPut("QuantityCheck/{orderItemId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> QuantityCheck(int orderItemId, [FromBody]QuantityCheckDto quantityCheck)
    {
        if (orderItemId == quantityCheck.OrderItemId)
        {
            var orderItem = await _orderItemRepository.GetOrderItem(orderItemId);

            if (orderItem != null)
            {

                orderItem.QuantityCheck = quantityCheck.QuantityCheck;

                if (orderItem.Quantity == orderItem.QuantityCheck)
                { 

                    if (await _orderItemRepository.UpdateOrderItem(orderItem))
                    {
                        return NoContent();
                    }
                }

                return BadRequest("Quantity does not equal the amount u added");
            }
        }

        return BadRequest();
    }

    [HttpDelete("{orderItemId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeleteOrderItem(int orderItemId)
    {
        if (!await _orderItemRepository.OrderItemExist(orderItemId))
        {
            return NotFound();
        }

        var orderItemDelete = await _orderItemRepository.GetOrderItem(orderItemId);

        if (!await _orderItemRepository.DeleteOrderItem(orderItemDelete))
        {
            return BadRequest();
        }

        return NoContent();
    }
}
