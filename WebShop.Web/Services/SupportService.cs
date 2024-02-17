using System.Net.Http;
using WebShop.Models.DTOs;
using WebShop.Models.DTOs.MailDtos;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class SupportService : ISupportService
{
    private readonly HttpClient _httpClient;

    public SupportService( IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WebShop.Api");
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
            var response = await _httpClient.GetAsync("api/Discounts");

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

    public async Task<List<SupportMailDto>> GetUsersSupportMail(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Reviews/Users-Support-emails/{userId}");

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
}

