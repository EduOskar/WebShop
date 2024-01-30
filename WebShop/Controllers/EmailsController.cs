using Microsoft.AspNetCore.Http;
using WebShop.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models.DTOs;
using System.Net.Mail;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmailsController : ControllerBase
{
    private readonly IEmailSenderRepository _emailSender;

    public EmailsController(IEmailSenderRepository emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost]
    [ProducesResponseType(204)]
    public async Task<ActionResult> SendEmail([FromBody] EmailDto email)
    {
        if (email != null)
        {
            await _emailSender.SendEmailAsync(email);

            return NoContent();
        }
        return BadRequest($"Error processing {email}");
    }
}
