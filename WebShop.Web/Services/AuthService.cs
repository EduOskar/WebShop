using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CurrentUser> CurrentUserInformation()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<CurrentUser>("/api/Auth");

            return result!;
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
}
