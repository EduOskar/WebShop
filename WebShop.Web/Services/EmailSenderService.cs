using System.Net.Http;
using WebShop.Models.DTOs.MailDtos;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly HttpClient _httpClient;

    public EmailSenderService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WebShop.Api");
    }
    public async Task<EmailDto> SendEmailAsync(EmailDto email)
    {
        var response = await _httpClient.PostAsJsonAsync<EmailDto>("api/Emails", email);

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null!;
            }

            var emailResponse = await response.Content.ReadFromJsonAsync<EmailDto>();

            return emailResponse!;
        }
        else
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status:{response.StatusCode} Message -{message}");
        }
    }
}
