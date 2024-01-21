using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IReviewServices
{
    Task<ReviewDto> CreateReview(ReviewDto reviewCreate);

    Task<bool> DeleteReview(int reviewId);

    Task<bool> UpdateReview(ReviewDto reviewUpdate);

    Task<List<ReviewDto>> GetRreviews();

    Task<ReviewDto> GetReview(int reviewId);

    Task<List<ReviewDto>> GetReviewsByProduct(int productId);
}
