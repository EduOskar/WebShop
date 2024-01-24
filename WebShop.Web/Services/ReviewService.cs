﻿using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class ReviewService : IReviewServices
{
    private readonly HttpClient _httpClient;

    public ReviewService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WebShop.Api");
    }

    public async Task<ReviewDto> CreateReview(ReviewDto reviewCreate)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<ReviewDto>("api/Reviews", reviewCreate);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var review = await response.Content.ReadFromJsonAsync<ReviewDto>();

                return review!;
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

    public async Task<bool> DeleteReview(int reviewId, int userId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Reviews/{reviewId}-{userId}");

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

    public async Task<ReviewDto> GetReview(int reviewId)
    {

        var response = await _httpClient.GetAsync($"api/Reviews/{reviewId}");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null!;
            }

            var review = await response.Content.ReadFromJsonAsync<ReviewDto>();
            return review!;
        }
        else
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status:{response.StatusCode} Message -{message}");
        }

    }

    public async Task<List<ReviewDto>> GetReviewsByProduct(int productId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Reviews/Get-Reviews-From-Product/{productId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var reviews = await response.Content.ReadFromJsonAsync<List<ReviewDto>>();
                return reviews!;
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

    public Task<List<ReviewDto>> GetRreviews()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateReview(ReviewDto reviewUpdate)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(reviewUpdate);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PutAsync($"api/Reviews/{reviewUpdate.Id}", content);

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
}
