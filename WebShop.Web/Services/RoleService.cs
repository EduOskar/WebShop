using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class RoleService : IRoleService
{
    private readonly HttpClient _httpClient;

    public RoleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RolesDto>> GetRoles()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Roles/Roles");

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
            var response = await _httpClient.GetAsync($"api/Roles/{userId}");

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
            var response = await _httpClient.GetAsync("api/Roles");

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

            var response = await _httpClient.PutAsync($"api/Roles/{userRole.UserId}", content);

            return response.IsSuccessStatusCode;

        }
        catch (Exception)
        {

            throw;
        }
    }
}
