﻿using WebShop.Api.Entity;
using WebShop.Models.DTOs.MailDtos;

namespace WebShop.Api.Repositories.Contracts;

public interface ISupportrepository
{
    Task<bool> AddSupportMessage(SupportMessages supportMessage);

    Task<List<SupportMessages>> SupportMessagesByTicket(int ticketId);

    Task<SupportMessages> GetSupportMessage(int messageId);

    Task<List<SupportMessages>> GetSupportMessagesForTicket(int supportMailId);

    Task<List<SupportMail>> GetSupportMails();

    Task<List<SupportMail>> GetUserSupportMails(int userId);

    Task<SupportMail> GetSupportMail(int id);

    Task<bool> UppdateSupportMail(SupportMail supportMail);

    Task<bool> CreateSupportMail(SupportMail supportMailCreate);

    Task<bool> DeleteSupportMail(SupportMail supportMailDelete);

    Task SendSupportEmailAsync(SupportMail supportEmail);

    Task<bool> CreateSupportTicket(MessageTicket messageTicket);

    Task<List<MessageTicket>> GetMEssageTicketsByUser(int userId);

    Task<MessageTicket> GetMessageTicket(int messageTicketId);

    Task<List<MessageTicket>> GetMessageTickets();

    Task<bool> Save();



}
