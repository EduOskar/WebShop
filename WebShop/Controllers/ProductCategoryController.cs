using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        public ProductCategoryController(IProductCategoryRepository productCategoryRepository, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ProductCategory>> GetProductCategories()
        {
            var productCategories = _mapper.Map<List<ProductCategoryDto>>(
                await _productCategoryRepository.GetCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(productCategories);
        }

        [HttpGet("{categoryId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int categoryId)
        {
            var productCategory = _mapper.Map<ProductCategoryDto>(
                await _productCategoryRepository.GetCategory(categoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(productCategory);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateProductCategory([FromBody] ProductCategoryDto productCategoryCreate)
        {
            if (productCategoryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var categories = await _productCategoryRepository.GetCategories();

            if (categories == null)
            {
                ModelState.AddModelError("", "Error fetching Categories");
                return BadRequest(ModelState);
            }

            var categoryCheck = categories
                .Where(u => u.Name.Trim().Equals(productCategoryCreate.Name!.TrimEnd(), StringComparison.CurrentCultureIgnoreCase))
                .Any();

            if (!categoryCheck)
            {
                ModelState.AddModelError("", "ProductCategory already exist");
                return StatusCode(422, ModelState);
            }

            var categoryMap = _mapper.Map<ProductCategory>(productCategoryCreate);

            if (!await _productCategoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created");
        }

        [HttpPut("{categoryId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateCategory(int categoryId, [FromBody] ProductCategoryDto updatedProductCategory)
        {
            if (updatedProductCategory == null)
            {
                return BadRequest(ModelState);
            }

            if (categoryId != updatedProductCategory.Id)
            {
                return BadRequest(ModelState);
            }

            if (!await _productCategoryRepository.CategoryExist(categoryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categoryMap = _mapper.Map<ProductCategory>(updatedProductCategory);

            if (!await _productCategoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoryId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            if (!await _productCategoryRepository.CategoryExist(categoryId))
            {
                return NotFound();
            }

            var categoryDelete = await _productCategoryRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _productCategoryRepository.DeleteCategory(categoryDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the category");
            }

            return NoContent();

        }
    }
}
