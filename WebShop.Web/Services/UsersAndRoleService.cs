using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class UsersAndRoleService : IUsersAndRoleService
{
    private readonly HttpClient _httpClient;

    public UsersAndRoleService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WebShop.Api");
    }

    public async Task<bool> DeleteUserRoles(int userId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/UsersAndRoles/{userId}");

            return response.IsSuccessStatusCode;

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<RolesDto> GetRole(int roleId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/UsersAndRoles/GetRole/{roleId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var Role = await response.Content.ReadFromJsonAsync<RolesDto>();
                return Role!;
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

    public async Task<List<RolesDto>> GetRoles()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/UsersAndRoles/Roles");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }
                var roles = await response.Content.ReadFromJsonAsync<List<RolesDto>>();
                return roles!;
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

    public async Task<UserRoleDto> GetUserRole(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/UsersAndRoles/{userId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var userRole = await response.Content.ReadFromJsonAsync<UserRoleDto>();
                return userRole!;
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

    public async Task<List<UserRoleDto>> GetUsersAndRoles()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/UsersAndRoles");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }
                var usersRoles = await response.Content.ReadFromJsonAsync<List<UserRoleDto>>();
                return usersRoles!;
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

    public async Task<bool> UpdateUserToRole(UserRoleDto userRole)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(userRole);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PutAsync($"api/UsersAndRoles/{userRole.UserId}", content);

            return response.IsSuccessStatusCode;

        }
        catch (Exception)
        {

            throw;
        }
    }
}
