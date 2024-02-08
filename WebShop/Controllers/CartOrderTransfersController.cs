using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebShop.Api.Entity;
using WebShop.Api.Repositories;
using WebShop.Api.Repositories.Contracts;
using WebShop.Api.Services;
using WebShop.Models.DTOs;
using OrderStatusType = WebShop.Api.Entity.OrderStatusType;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CartOrderTransfersController : ControllerBase
{
    private readonly CartOrderTransferService _cartOrderTransferService;
    private readonly IEmailSenderRepository _emailSenderRepository;

    public CartOrderTransfersController(CartOrderTransferService cartOrderTransferService, IEmailSenderRepository emailSenderRepository)
    {
        _cartOrderTransferService = cartOrderTransferService;
        _emailSenderRepository = emailSenderRepository;
    }

    [HttpGet("{userId:int}/{discountCode?}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<bool>> CartOrderTransfer(int userId, string? discountCode = null)
    {
        try
        {
            bool result = await _cartOrderTransferService.CartOrderTransfer(userId, discountCode);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound(result);
            }
        }
        catch (Exception ex)
        {

            return StatusCode(500, ex.Message);
        }

    }
}
