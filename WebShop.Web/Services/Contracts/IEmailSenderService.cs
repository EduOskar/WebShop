using WebShop.Models.DTOs.MailDtos;

namespace WebShop.Web.Services.Contracts;

public interface IEmailSenderService
{
    Task<EmailDto> SendEmailAsync(EmailDto email);
}
