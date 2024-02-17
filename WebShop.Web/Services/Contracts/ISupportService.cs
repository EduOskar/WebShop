using WebShop.Models.DTOs.MailDtos;

namespace WebShop.Web.Services.Contracts;

public interface ISupportService
{
    Task<List<SupportMailDto>> GetSupportMails();

    Task<SupportMailDto> GetSupportMail(int id);

    Task<List<SupportMailDto>> GetUsersSupportMail(int userId);

    Task<SupportMailDto> CreateSupportMail(SupportMailDto supportMail);

    Task<bool> DeleteSupportMail(int supportMailId);


}
