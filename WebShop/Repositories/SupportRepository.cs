using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs.MailDtos;

namespace WebShop.Api.Repositories;

public class SupportRepository : ISupportrepository
{
    private readonly ApplicationDbContext _dbContext;

    public SupportRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CreateSupportMail(SupportMail supportMailCreate)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == supportMailCreate.UserId);

        supportMailCreate.User = user!;

        await SendSupportEmailAsync(supportMailCreate);

        await _dbContext.SupportMails.AddAsync(supportMailCreate);

        return await Save();
    }

    public async Task<bool> DeleteSupportMail(SupportMail supportMailDelete)
    {

        _dbContext.Remove(supportMailDelete);

        return await Save();
    }

    public async Task<SupportMail> GetSupportMail(int id)
    {
        var supportMail = await _dbContext.SupportMails
            .Include(u => u.User)
            .Include(s => s.Support)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (supportMail != null)
        {
            return supportMail;
        }

        throw new Exception($"Could not find {supportMail}");
    }

    public async Task<List<SupportMail>> GetSupportMails()
    {
        var supportMails = await _dbContext.SupportMails
            .Include(u => u.User)
            .Include(s => s.Support)
            .ToListAsync();

        if (supportMails != null)
        {
            return supportMails;
        }

        throw new Exception($"Could not fetch List: {supportMails}");
    }

    public async Task<List<SupportMail>> GetUserSupportMails(int userId)
    {
        var userSupportMails = await _dbContext.SupportMails
            .Include(u => u.User)
            .Include(s => s.Support)
            .Where(usm => usm.UserId == userId)
            .ToListAsync();

        if (userSupportMails != null)
        {
            return userSupportMails;
        }

        throw new Exception($"Could not fetch Users Supportmails");
    }

    public async Task<bool> Save()
    {
        var saved = await _dbContext.SaveChangesAsync();

        return saved > 0;
    }

    public Task SendSupportEmailAsync(SupportMail supportEmail)
    {
        if (supportEmail != null)
        {
            var mail = "Consid_Support@Consid.com";
            var host = "localhost";
            var port = 25;
            var sender = supportEmail.User.Email;

            var client = new SmtpClient(host, port);

            var mailSent = client.SendMailAsync(
                new MailMessage(
                    from: sender!,
                    to: mail!,
                    supportEmail.Subject,
                    supportEmail.Body));
            return mailSent;
        }

        throw new Exception("Email could not be  sent due to missing information");
    }
}
