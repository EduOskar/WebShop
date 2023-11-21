using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, 
            IProductCategoryRepository productCategoryRepository, 
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(
                await _productRepository.GetProducts());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(products);
        }

        [HttpGet("{productId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            if (!await _productRepository.ProductExist(productId))
            {
                return NotFound();
            }

            var product = _mapper.Map<ProductDto>(
                await _productRepository.GetProduct(productId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(product);
        }

        [HttpGet("{categoryId:int}/GetProducts-By-Category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Product>> GetProductsByCategory(int categoryId)
        {
            var productsByCategory = _mapper.Map<List<ProductDto>>(
                await _productRepository.GetProductByCategory(categoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(productsByCategory);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateProduct([FromBody] ProductDto productCreate)
        {
            if (productCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryExist = await _productCategoryRepository.CategoryExist(productCreate.CategoryId);

            if (!categoryExist)
            {
                ModelState.AddModelError("", $"ProductCategory with Id {productCreate.CategoryId} does not exist");
                return BadRequest(ModelState);
            }

            var productMap = _mapper.Map<Product>(productCreate);
            productMap.Category = await _productCategoryRepository.GetCategory(productCreate.CategoryId);

            if (!await _productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "There was an error saving");
                return BadRequest(ModelState);
            }

            return Created();
        }

        [HttpPut("{productId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateProduct(int productId, [FromBody] ProductDto updateProduct)
        {
            if (updateProduct == null)
            {
                return BadRequest(ModelState);
            }

            if (productId != updateProduct.Id)
            {
                return BadRequest(ModelState);
            }

            if (!await _productRepository.ProductExist(productId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productMap = _mapper.Map<Product>(updateProduct);

            if (!await _productRepository.UpdateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{productId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            if (!await _productRepository.ProductExist(productId))
            {
                return NotFound();
            }

            var productDelete = await _productRepository.GetProduct(productId);

            if (!await _productRepository.DeleteProduct(productDelete))
            {
                return BadRequest();
            }

            return NoContent();

        }
    }
}
