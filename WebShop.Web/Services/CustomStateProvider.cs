using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class CustomStateProvider : AuthenticationStateProvider
{
    private readonly IAuthService _api;
    private CurrentUser? _currentUser;

    public CustomStateProvider(IAuthService api)
    {
        _api = api;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        try
        {
            var userInfo = await GetCurrentUser();

            if (userInfo.IsAuthenticated)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, _currentUser!.UserName)
                }.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));

                identity = new ClaimsIdentity(claims, "Server authentication");
            }
        }
        catch (HttpRequestException ex)
        {

             Console.WriteLine("Request failed" + ex);
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private async Task<CurrentUser> GetCurrentUser()
    {
        if (_currentUser != null && _currentUser.IsAuthenticated)
        {
            return _currentUser;
        }

        _currentUser = await _api.CurrentUserInformation();

        return _currentUser;
    }

    public async Task Logout()
    {
        await _api.Logout();

        _currentUser = null;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task Login(LoginRequest LoginParameters)
    {
        await _api.Login(LoginParameters);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task Register(UserDto registerParameters)
    {
        await _api.Register(registerParameters);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
