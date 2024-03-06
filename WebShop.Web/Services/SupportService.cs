using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using WebShop.Models.DTOs;
using WebShop.Models.DTOs.MailDtos;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class SupportService : ISupportService
{
    private readonly HttpClient _httpClient;

    public SupportService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WebShop.Api");
    }

    public async Task<SupportMessagesDto> AddSupportMessage(int supportMailId, SupportMessagesDto supportMessage)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<SupportMessagesDto>($"api/Supports/messages/{supportMailId}", supportMessage);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var mailreturn = await response.Content.ReadFromJsonAsync<SupportMessagesDto>();

                return mailreturn!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} Message -{message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<bool> AssignSupportToEmail(int supportMailId, int supportId)
    {
        var response = await _httpClient.PostAsync($"api/Supports/AssignSupportToMail/{supportMailId}/{supportId}", null);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            return bool.Parse(responseContent);
        }
        else
        {
            return response.IsSuccessStatusCode;
        }
    }

    public async Task<bool> AssignSupportToTicket(int ticketId, int supportId)
    {
        var response = await _httpClient.PostAsync($"api/Supports/AssignSupportToTicket/{ticketId}/{supportId}", null);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            return bool.Parse(responseContent);
        }
        else
        {
            return response.IsSuccessStatusCode;
        }
    }

    public async Task<SupportMailDto> CreateSupportMail(SupportMailDto supportMail)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<SupportMailDto>("api/Supports", supportMail);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var mailreturn = await response.Content.ReadFromJsonAsync<SupportMailDto>();

                return mailreturn!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} Message -{message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<bool> DeleteSupportMail(int supportMailId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Supports/{supportMailId}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error updating review. Status code: {response.StatusCode}. Response content: {errorContent}");
                return false;
            }

            return true;

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Request Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
            throw;
        }
    }

    public async Task<SupportMailDto> GetSupportMail(int id)
    {
        var response = await _httpClient.GetAsync($"api/Supports/{id}");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null!;
            }

            var supportMail = await response.Content.ReadFromJsonAsync<SupportMailDto>();
            return supportMail!;
        }
        else
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status:{response.StatusCode} Message -{message}");
        }
    }

    public async Task<List<SupportMailDto>> GetSupportMails()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Supports");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var supportMails = await response.Content.ReadFromJsonAsync<List<SupportMailDto>>();
                return supportMails!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<SupportMessagesDto>> GetSupportMessagesForMail(int supportMailId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Supports/messages/{supportMailId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var supportMails = await response.Content.ReadFromJsonAsync<List<SupportMessagesDto>>();
                return supportMails!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
    public async Task<List<SupportMailDto>> GetUsersSupportMail(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Supports/Users-Support-emails/{userId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var supportMails = await response.Content.ReadFromJsonAsync<List<SupportMailDto>>();
                return supportMails!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<bool> UpdateSupportMail(int supportMailId)
    {
        var response = await _httpClient.PutAsync($"api/Supports/{supportMailId}", null);

        return response.IsSuccessStatusCode;
    }

    public async Task<MessageTicketDto> CreateSupportTicket(MessageTicketDto messageTicket)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<MessageTicketDto>("api/Supports/Tickets", messageTicket);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var ticket = await response.Content.ReadFromJsonAsync<MessageTicketDto>();

                return ticket!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} Message -{message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<MessageTicketDto>> GetMessageTicketsByUser(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Supports/UserTickets/{userId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var tickets = await response.Content.ReadFromJsonAsync<List<MessageTicketDto>>();

                return tickets!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<MessageTicketDto> GetMessageTicket(int messageTicketId)
    {
        var response = await _httpClient.GetAsync($"api/Supports/Tickets/{messageTicketId}");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null!;
            }

            var Ticket = await response.Content.ReadFromJsonAsync<MessageTicketDto>();
            return Ticket!;
        }
        else
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status:{response.StatusCode} Message -{message}");
        }
    }

    public async Task<List<MessageTicketDto>> GetMessageTickets()
    {
        var response = await _httpClient.GetAsync("api/Supports/Tickets");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null!;
            }

            var tickets = await response.Content.ReadFromJsonAsync<List<MessageTicketDto>>();

            return tickets!;
        }
        else
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status:{response.StatusCode} Message -{message}");
        }
    }
}

