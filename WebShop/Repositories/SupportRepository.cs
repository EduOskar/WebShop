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
    public async Task<bool> AddSupportMessage(SupportMessages supportMessage)
    {
        await _dbContext.SupportMessages.AddAsync(supportMessage);
        return await Save();
    }

    public async Task<List<SupportMessages>> GetSupportMessagesForMail(int supportMailId)
    {
        return await _dbContext.SupportMessages
                                .OrderBy(m => m.CreatedAt)
                                .ToListAsync();
    }
    public async Task<bool> CreateSupportMail(SupportMail supportMailCreate)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == supportMailCreate.UserId);

        supportMailCreate.User = user!;
        supportMailCreate.IsAnswered = Entity.IsAnswered.NotAnswered;

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

    public async Task<bool> UppdateSupportMail(SupportMail supportMail)
    {
        _dbContext.Update(supportMail);

        return await Save();
    }

    public async Task<List<SupportMail>> GetSupportMails()
    {
        var supportMails = await _dbContext.SupportMails
            .Include(u => u.User)
            .Include(s => s.Support)
            .OrderBy(x => x.DateTime)
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
            .OrderBy(x => x.DateTime)
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
            var sender = supportEmail.User!.Email;

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

    public async Task<bool> CreateSupportTicket(MessageTicket messageTicket)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == messageTicket.UserID);

        messageTicket.User = user!;
        messageTicket.IsResolved = Entity.IsResolved.NotResolved;

        await _dbContext.MessageTickets.AddAsync(messageTicket);

        return await Save();
    }

    public async Task<List<MessageTicket>> GetMEssageTicketsByUser(int userId)
    {
        var userMessageTickets = await _dbContext.MessageTickets
            .OrderBy(x => x.CreatedDate)
            .Where(x => x.UserID == userId).ToListAsync();

        if (userMessageTickets != null)
        {
            return userMessageTickets;
        }

        throw new Exception("Couldnt find any userMEssageTickets with that user");
    }

    public async Task<MessageTicket> GetMessageTicket(int messageTicketId)
    {
        var messageTicket = await _dbContext.MessageTickets
            .Include(x => x.User)
            .Include(x => x.Support)
            .FirstOrDefaultAsync(x => x.Id == messageTicketId);

        if (messageTicket != null)
        {
            return messageTicket;
        }

        throw new Exception($"Couldnt find messageticket with Id{messageTicket}");
    }

    public async Task<List<MessageTicket>> GetMessageTickets()
    {
        var messageTickets = await _dbContext.MessageTickets
            .Include(x => x.User)
            .Include(x => x.Support)
            .OrderBy(x => x.CreatedDate).ToListAsync();

        return messageTickets;
    }
}

