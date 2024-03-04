using WebShop.Models.DTOs.MailDtos;

namespace WebShop.Web.Services.Contracts;

public interface ISupportService
{
    Task<SupportMessagesDto> AddSupportMessage(int supportMailId, SupportMessagesDto supportMessage);

    Task<List<SupportMessagesDto>> GetSupportMessagesForMail(int supportMailId);

    Task<List<SupportMailDto>> GetSupportMails();

    Task<SupportMailDto> GetSupportMail(int id);

    Task<List<SupportMailDto>> GetUsersSupportMail(int userId);

    Task<SupportMailDto> CreateSupportMail(SupportMailDto supportMail);

    Task<bool> DeleteSupportMail(int supportMailId);

    Task<bool> AssignSupportToTicket(int supportMailId, int supportId);

    Task<bool> UpdateSupportMail(int supportMailId);
}
