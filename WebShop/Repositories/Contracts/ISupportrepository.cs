using WebShop.Api.Entity;
using WebShop.Models.DTOs.MailDtos;

namespace WebShop.Api.Repositories.Contracts;

public interface ISupportrepository
{
    Task<List<SupportMail>> GetSupportMails();

    Task<List<SupportMail>> GetUserSupportMails(int userId);

    Task<SupportMail> GetSupportMail(int id);

    Task<bool> CreateSupportMail(SupportMail supportMailCreate);

    Task<bool> DeleteSupportMail(SupportMail supportMailDelete);

    Task SendSupportEmailAsync(SupportMail supportEmail);

    Task<bool> Save();



}
