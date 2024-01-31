using WebShop.Models.DTOs;
using WebShop.Models.DTOs.PasswordManagement;
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

    public async Task ForgotPassword(ForgotPasswordDto forgotPassword)
    {
        var result = await _httpClient.PostAsJsonAsync("api/Auth/Forgot-Password", forgotPassword);

        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }

        result.EnsureSuccessStatusCode();
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

    public async Task ResetPassword(ResetPasswordDto resetPassword)
    {
        var result = await _httpClient.PostAsJsonAsync("/api/Auth/Reset-Password", resetPassword);

        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }

        result.EnsureSuccessStatusCode();
    }

}
