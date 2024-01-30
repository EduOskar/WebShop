using System.Net.Mail;
using System.Text;
using WebShop.Models.DTOs;

namespace WebShop.Api.Repositories.Contracts;

public interface IEmailSenderRepository
{
    Task SendEmailAsync(EmailDto email);
}
