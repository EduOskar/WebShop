﻿using Newtonsoft.Json;
using System.Text;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class UsersServices : IUsersService
{
    private readonly HttpClient _httpClient;

    public UsersServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
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

    public async Task<UserDto> DeleteUser(int userId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Users/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var userDelete = await response.Content.ReadFromJsonAsync<UserDto>();

                return userDelete!;
            }

            return null!;
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
            //Förändra när jag implementerar inloggning
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

    public async Task<ICollection<UserDto>> GetUsers()
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
                var users = await response.Content.ReadFromJsonAsync<ICollection<UserDto>>();
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

    public async Task<UserDto> UpdateUser(UserDto userUpdate)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(userUpdate);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PutAsync($"api/Products/{userUpdate.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserDto>();

                return user!;
            }

            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

