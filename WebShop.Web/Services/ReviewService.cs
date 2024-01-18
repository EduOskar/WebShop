using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class ReviewService : IReviewServices
{
    private readonly HttpClient _httpClient;

    public ReviewService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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

    public async Task<bool> DeleteReview(int reviewId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Reviews/{reviewId}");

            return response.IsSuccessStatusCode;

        }
        catch (Exception)
        {

            throw;
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

            return response.IsSuccessStatusCode;

        }
        catch (Exception)
        {

            throw;
        }
    }
}
