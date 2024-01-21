using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ReviewsController(IReviewRepository reviewRepository, 
        IUserRepository userRepository, 
        IProductRepository productRepository,
        IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
    {
        var reviews = _mapper.Map<List<ReviewDto>>(await _reviewRepository.GetReviews());

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(reviews);
    }


    [HttpGet("{reviewId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ReviewDto>> GetReview(int reviewId)
    {
        if (!await _reviewRepository.ReviewExist(reviewId))
        {
            return NotFound();
        }

        var review = _mapper.Map<ReviewDto>(await _reviewRepository.GetReview(reviewId));

        if (review == null)
        {
            return BadRequest();
        }

        return Ok(review);
    }

    [HttpGet("{userId:int}/GetReviews-by-User")]
    [ProducesResponseType(200)]
    public async Task<ActionResult<ReviewDto>> GetReviewsByUsers(int userId)
    {
        var userReview = _mapper.Map<List<ReviewDto>>(await _reviewRepository.GetReviewsFromUser(userId));

        if (userReview == null)
        {
            return BadRequest();
        }

        return Ok(userReview);
    }

    [HttpGet("Get-Reviews-From-Product/{productId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ReviewDto>> GetReviewsFromProduct(int productId)
    {
        var reviews = await _reviewRepository.GetReviewsByProduct(productId);

        if (reviews != null)
        {
            return Ok(reviews);
        }

        return BadRequest();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> CreateReview([FromBody] ReviewDto reviewCreate)
    {
        if (reviewCreate == null)
        {
            return BadRequest();
        }

        var productExist = await _productRepository.ProductExist(reviewCreate.ProductId);

        if (!productExist)
        {
            ModelState.AddModelError("", $"Product with Id {reviewCreate.ProductId} does not exist");
            return BadRequest(ModelState);
        }

        var userExist = await _userRepository.UserExist(reviewCreate.UserId);

        if (!userExist)
        {
            return BadRequest();
        }


        var reviewMap = _mapper.Map<Review>(reviewCreate);
        reviewMap.Product = await _productRepository.GetProduct(reviewCreate.ProductId);
        reviewMap.CreatedAt = DateTime.Now;
        reviewMap.User = await _userRepository.GetUser(reviewCreate.UserId);

        if (!await _reviewRepository.CreateReview(reviewMap))
        {
            return BadRequest("There was an error saving");
        }

        return CreatedAtAction("GetReview", new {reviewId = reviewCreate.Id}, reviewCreate);
    }

    [HttpPut("{reviewId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(403)]
    public async Task<ActionResult> UpdateReview(int reviewId, ReviewDto updateReview)
    {
        if (updateReview == null)
        {
            return BadRequest();
        }

        if (reviewId != updateReview.Id)
        {
            return BadRequest();
        }

        var user = await _userRepository.GetUser(updateReview.UserId);

        var product = await _productRepository.GetProduct(updateReview.ProductId);

        if (updateReview.UserId != user.Id)
        {
            return  Forbid();
        }

        if (!await _reviewRepository.ReviewExist(reviewId))
        {
            return NotFound();
        }

        var reviewMap = _mapper.Map<Review>(updateReview);
        reviewMap.User = user;
        reviewMap.Product = product;

        if (!await _reviewRepository.UpdateReview(reviewMap))
        {
            return BadRequest("Something went wrong while updating");
        }

        return NoContent();
    }

    [HttpDelete("{reviewId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeleteReview(int reviewId)
    {
        if (!await _reviewRepository.ReviewExist(reviewId))
        {
            return NotFound();
        }

        var reviewDelete = await _reviewRepository.GetReview(reviewId);

        if (!await _reviewRepository.DeleteReview(reviewDelete))
        {
            return BadRequest();
        }

        return NoContent();

    }

}
