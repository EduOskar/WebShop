using Microsoft.AspNetCore.Http;
using WebShop.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using WebShop.Models.DTOs.MailDtos;

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
    [ProducesResponseType(500)]
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
