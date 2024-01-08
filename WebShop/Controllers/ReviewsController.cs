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
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
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
    public async Task<ActionResult<Review>> GetReview(int reviewId)
    {
        if (!await _reviewRepository.ReviewExist(reviewId))
        {
            return NotFound();
        }

        var review = _mapper.Map<ReviewDto>(await _reviewRepository.GetReview(reviewId));

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(review);
    }

    [HttpGet("{userId:int}/GetReviews-by-User")]
    [ProducesResponseType(200)]
    public async Task<ActionResult<Review>> GetReviewsByUsers(int userId)
    {
        var userReview = _mapper.Map<List<ReviewDto>>(await _reviewRepository.GetReviewsFromUser(userId));

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(userReview);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> CreateReview([FromBody] ReviewDto reviewCreate)
    {
        if (reviewCreate == null)
        {
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userExist = await _userRepository.UserExist(reviewCreate.UserId);

        if (!userExist)
        {
            ModelState.AddModelError("", $"User with Id {reviewCreate.UserId} does not exist");
            return BadRequest(ModelState);
        }

        var productExist = await _productRepository.ProductExist(reviewCreate.ProductId);

        if (!productExist)
        {
            ModelState.AddModelError("", $"Product with Id {reviewCreate.ProductId} does not exist");
            return BadRequest(ModelState);
        }


        var reviewMap = _mapper.Map<Review>(reviewCreate);
        //reviewMap.User = await _userRepository.GetUser(reviewCreate.UserId);
        reviewMap.Product = await _productRepository.GetProduct(reviewCreate.ProductId);

        if (!await _reviewRepository.CreateReview(reviewMap))
        {
            ModelState.AddModelError("", "There was an error saving");
            return BadRequest(ModelState);
        }

        return CreatedAtAction("GetReview", new {reviewId = reviewCreate.Id}, reviewCreate);
    }

    [HttpPut("{reviewId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateReview(int reviewId, [FromBody] ReviewDto updateReview)
    {
        if (updateReview == null)
        {
            return BadRequest(ModelState);
        }

        if (reviewId != updateReview.Id)
        {
            return BadRequest(ModelState);
        }

        if (!await _reviewRepository.ReviewExist(reviewId))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var reviewMap = _mapper.Map<Review>(updateReview);

        if (!await _reviewRepository.UpdateReview(reviewMap))
        {
            ModelState.AddModelError("", "Something went wrong while updating");
            return BadRequest(ModelState);
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
