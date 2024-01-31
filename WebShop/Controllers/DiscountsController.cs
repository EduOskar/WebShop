﻿using AutoMapper;
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
    private readonly IEmailSenderRepository _emailSenderRepository;

    public DiscountsController(IMapper mapper,
        IDiscountRepository discountRepository,
        IUserRepository userRepository,
        IEmailSenderRepository emailSenderRepository)
    {
        _mapper = mapper;
        _discountRepository = discountRepository;
        _userRepository = userRepository;
        _emailSenderRepository = emailSenderRepository;
    }

    [HttpGet("{discountCode}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<DiscountDto>> GetDiscount(string discountCode)
    {
        var discount = _mapper.Map<DiscountDto>(await _discountRepository.GetDiscount(discountCode));

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
    public async Task<ActionResult> CreateDiscount([FromBody] DiscountDto discountCreate)
    {
        if (discountCreate != null)
        {
            var newCode = _discountRepository.GenerateUniqueCode(7, discountCreate.DiscountCode);

            var discountMap = _mapper.Map<Discount>(discountCreate);
            discountMap.DiscountCode = newCode;
            discountMap.IsActive = (Entity.DiscountStatus)1;
            discountMap.DiscountsUsed = 0;

            if (await _discountRepository.CreateDiscount(discountMap))
            {
                return CreatedAtAction("GetDiscount", new { discountCode = discountMap.DiscountCode }, discountMap);
            }

        }
        return BadRequest();
    }

    [HttpPost("apply-discount/{userId:int}-{discountCode}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> ApplyDiscount(int userId, string discountCode)
    {

        bool canUseDiscount = await _discountRepository.CanUserUseDiscount(userId, discountCode);

        if (canUseDiscount)
        {
            var discountIsActive = await _discountRepository.GetDiscount(discountCode);
            discountIsActive.DiscountsUsed++;

            if (discountIsActive.IsActive == Entity.DiscountStatus.Active)
            {
                await _discountRepository.RecordDiscountUsage(userId, discountCode);

                return Ok("Discount applied successfully.");
            }

        }
        return BadRequest("Discount cannot be applied: either it's invalid, expired, or already used.");
    }

    [HttpPut("{discountId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> UpdateDiscount(int discountId, [FromBody] DiscountDto discountUpdate)
    {
        if (discountId == discountUpdate.Id)
        {
            var discountMap = _mapper.Map<Discount>(discountUpdate);

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

    [HttpGet("Email-discounts/{discountCode}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> EmailDiscounts(string discountCode)
    {
        var discount = await _discountRepository.GetDiscount(discountCode);

        if (discount != null)
        {
            var userList = await _userRepository.GetUsers();

            if (userList.Any())
            {
                EmailDto email = new EmailDto();

                foreach (var user in userList)
                {
                    email.To = user.Email!;
                    email.From = "";
                    email.Body = $"DiscountCode for consid_WebShop: {discount.DiscountCode}";
                    email.Subject = "Discount at Consids Webbshop! ConsidWebbshop@Consid.com";
                    await _emailSenderRepository.SendEmailAsync(email);
                }

                return Ok();
            }

            return BadRequest();
        }
        return NotFound();
    }

}
