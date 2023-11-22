using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IReviewsService
{
    Task<ICollection<ReviewDto>> GetReviews();
    Task<ReviewDto> GetReview(int reviewId);
    Task<ICollection<ReviewDto>> GetReviewsFromUser(int userId);
    Task<bool> ReviewExist(int reviewId);
    Task<bool> CreateReview(ReviewDto review);
    Task<bool> UpdateReview(ReviewDto review);
    Task<bool> DeleteReview(ReviewDto review);
    Task<bool> DeleteReviews(List<ReviewDto> reviews);
    Task<bool> Save();
}
