using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DiscountsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IDiscountRepository _discountRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserRolesRepository _userRolesRepository;

    public DiscountsController(IMapper mapper,
        IDiscountRepository discountRepository,
        IUserRepository userRepository,
        IUserRolesRepository userRolesRepository)
    {
        _mapper = mapper;
        _discountRepository = discountRepository;
        _userRepository = userRepository;
        _userRolesRepository = userRolesRepository;
    }

    [HttpGet("{discountId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<DiscountDto>> GetDiscount(int discountId)
    {
        var discount = _mapper.Map<DiscountDto>(await _discountRepository.GetDiscount(discountId));

        if (discount != null)
        {
            return Ok(discount);
        }

        return NotFound();
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<DiscountDto>>> GetDiscounts()
    {
        var discounts = _mapper.Map<List<DiscountDto>>(await _discountRepository.GetDiscounts());

        if (discounts != null)
        {
            return Ok(discounts);
        }

        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> CreateDiscount(DiscountDto discountCreate)
    {
        if (discountCreate != null)
        {
            var newCode = _discountRepository.GenerateUniqueCode(7, discountCreate.DiscountCode);

            var discountMap = _mapper.Map<Discount>(discountCreate);
            discountMap.DiscountCode = newCode;

            if (await _discountRepository.CreateDiscount(discountMap))
            {
                return CreatedAtAction("GetDiscount", new { discountId = discountMap.Id }, discountMap);
            }
        }
        return BadRequest();
    }

    [HttpPost("apply-discount/{userId:int}-{discountCode}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> ApplyDiscount(int userId, string discountCode)
    {

        bool canUseDiscount = await _discountRepository.CanUserUseDiscount(userId, discountCode);

        if (!canUseDiscount)
        {
            return BadRequest("Discount cannot be applied: either it's invalid, expired, or already used.");
        }

        await _discountRepository.RecordDiscountUsage(userId, discountCode);

        return Ok("Discount applied successfully.");

    }

    [HttpPut("{discountId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> UpdateDiscount(int discountId, [FromBody] DiscountDto discountUpdate)
    {
        if (discountId == discountUpdate.Id)
        {
            var newCode = _discountRepository.GenerateUniqueCode(7, discountUpdate.DiscountCode);

            var discountMap = _mapper.Map<Discount>(discountUpdate);
            discountMap.DiscountCode = newCode;

            if (discountMap != null)
            {
                if (await _discountRepository.UpdateDiscount(discountMap))
                {
                    return NoContent();
                }
            }

            return NotFound();
        }

        return BadRequest();
    }
}
