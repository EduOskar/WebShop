using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IEmailSenderService
{
    Task<EmailDto> SendEmailAsync(EmailDto email);
}
