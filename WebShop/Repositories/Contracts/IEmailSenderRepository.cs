using System.Net.Mail;
using System.Text;
using WebShop.Models.DTOs.MailDtos;

namespace WebShop.Api.Repositories.Contracts;

public interface IEmailSenderRepository
{
    Task SendEmailAsync(EmailDto email);
}
