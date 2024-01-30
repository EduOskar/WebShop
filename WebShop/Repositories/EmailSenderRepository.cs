using System.Net;
using System.Net.Mail;

using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Repositories;

public class EmailSenderRepository : IEmailSenderRepository
{
    public Task SendEmailAsync(EmailDto email)
    {
        var mail = "Consid_WebbShop@Consid.Com";

        var host = "localhost";
        var port = 25;

        var client = new SmtpClient(host, port);

        var mailSent =  client.SendMailAsync(
            new MailMessage(
                from: mail,
                to: email.To,
                email.Subject,
                email.Body));

        return (mailSent);
    }
}
