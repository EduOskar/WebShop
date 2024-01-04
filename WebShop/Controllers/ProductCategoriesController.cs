using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductCategoriesController : ControllerBase
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IMapper _mapper;

    public ProductCategoriesController(IProductCategoryRepository productCategoryRepository, IMapper mapper)
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
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> CreateProductCategory([FromBody] ProductCategoryDto productCategoryCreate)
    {
        if (productCategoryCreate == null)
        {
            return BadRequest("Something went wrong");
        }

        var categories = await _productCategoryRepository.GetCategories();

        if (categories == null)
        {
            return BadRequest("Error fetching Categories");
        }

        bool categoryCheck = categories
            .Where(u => u.Name.Trim().Equals(productCategoryCreate.Name!.TrimEnd(), StringComparison.CurrentCultureIgnoreCase))
            .Any();

        if (categoryCheck)
        {
            return StatusCode(422, "ProductCategory already exist");
        }

        var categoryMap = _mapper.Map<ProductCategory>(productCategoryCreate);

        if (!await _productCategoryRepository.CreateCategory(categoryMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return CreatedAtAction("GetProductCategory", new { categoryId = productCategoryCreate.Id }, productCategoryCreate);
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
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeleteCategory(int categoryId)
    {
        if (!await _productCategoryRepository.CategoryExist(categoryId))
        {
            return NotFound();
        }

        var categoryDelete = await _productCategoryRepository.GetCategory(categoryId);

        if (!await _productCategoryRepository.DeleteCategory(categoryDelete))
        {
            return BadRequest();
        }

        return NoContent();

    }
}
