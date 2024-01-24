using Newtonsoft.Json;
using System.Text;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class UsersServices : IUsersService
{
    private readonly HttpClient _httpClient;

    public UsersServices(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WebShop.Api");
    }

    public async Task<UserDto> CreateUser(UserDto userCreate)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<UserDto>("api/Users", userCreate);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var user = await response.Content.ReadFromJsonAsync<UserDto>();

                return user!;
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

    public async Task<bool> DeleteUser(int userId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Users/{userId}");

            return response.IsSuccessStatusCode;

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<UserDto> GetUser(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Users/{userId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var user = await response.Content.ReadFromJsonAsync<UserDto>();
                return user!;
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

    public async Task<List<UserDto>> GetUsers()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Users");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }
                var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
                return users!;
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

    public async Task<bool> UpdateUser(UserDto userUpdate)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(userUpdate);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PutAsync($"api/Users/{userUpdate.Id}", content);

            return response.IsSuccessStatusCode;
           
        }
        catch (Exception)
        {

            throw;
        }
    }
}

