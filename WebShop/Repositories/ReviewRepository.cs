using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReviewRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateReview(Review review)
        {
            await _dbContext.AddAsync(review);

            return await Save();
        }

        public async Task<bool> DeleteReview(Review review)
        {
            _dbContext.Remove(review);

            return await Save();
        }

        public async Task<bool> DeleteReviews(List<Review> reviews)
        {
            _dbContext.RemoveRange(reviews);

            return await Save();
        }

        public async Task<Review> GetReview(int reviewId)
        {
            var review = await _dbContext.Reviews.FindAsync(reviewId);

            if (review != null)
            {
                return review;
            }

            throw new Exception("Review was not found");
        }

        public async Task<ICollection<Review>> GetReviews()
        {
            var reviews = await _dbContext.Reviews
                .Include(x => x.User)
                .Include(x => x.Product)
                .ToListAsync();

            return reviews;
        }

        public async Task<List<Review>> GetReviewsByProduct(int productId)
        {
            var reviewsByUser = await _dbContext.Reviews
                .Include(x => x.User)
                .Include(x => x.Product)
                .Where(r => r.ProductId == productId).ToListAsync();

            if (reviewsByUser != null)
            {
                return reviewsByUser;
            }

            throw new Exception("Review was not found");
        }

        public async Task<ICollection<Review>> GetReviewsFromUser(int userId)
        {
            var userReview = await _dbContext.Reviews
                .Where(u => u.User!.Id == userId).ToListAsync();

            return userReview;
        }

        public async Task<bool> ReviewExist(int reviewId)
        {
            var reviewExist = await _dbContext.Reviews.AnyAsync(r => r.Id == reviewId);

            return reviewExist;
        }

        public async Task<bool> Save()
        {
            var saved = await _dbContext.SaveChangesAsync();

            return saved > 0;
        }

        public async Task<bool> UpdateReview(Review review)
        {
            _dbContext.Update(review);

            return await Save();
        }
    }
}
