using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient.CreateClient("WebShop.Api");
    }

    public async Task<CurrentUser> CurrentUserInformation()
    {

        try
        {
            var response = await _httpClient.GetFromJsonAsync<CurrentUser>("api/Auth/CurrentUserInformation");

            if (response == null)
            {
                throw new Exception("Something went wrong retrieving current user");
            }

            return response;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task Login(LoginRequest loginRequest)
    {
        var result = await _httpClient.PostAsJsonAsync("/api/Auth/login", loginRequest);

        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }

        result.EnsureSuccessStatusCode();
    }

    public async Task Logout()
    {
        var result = await _httpClient.PostAsync("/api/Auth/logout", null);

        result.EnsureSuccessStatusCode();
    }

    public async Task Register(UserDto registerRequest)
    {
        var result = await _httpClient.PostAsJsonAsync("/api/Auth/register", registerRequest);

        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }

        result.EnsureSuccessStatusCode();
    }

    //public async Task<UserDto> GetUserInformation(int userId)
    //{
    //    try
    //    {
    //        var response = await _httpClient.GetAsync($"api/Auth/GetUser");

    //        if (response.IsSuccessStatusCode)
    //        {
    //            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
    //            {
    //                return null!;
    //            }

    //            var user = await response.Content.ReadFromJsonAsync<UserDto>();
    //            return user!;
    //        }
    //        else
    //        {
    //            var message = await response.Content.ReadAsStringAsync();
    //            throw new Exception(message);
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
}
