using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.API.DTOs;
using Products.API.Models;
using Products.API.Services;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();
            if (products is null)
                return NotFound("Products not found");
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product is null)
                return NotFound("Products not found");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest("Product can't be null");

            await _productService.Create(productDTO);
            return new CreatedAtRouteResult("GetProduct", new { id = productDTO.id}, productDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest("Product can't be null");

            await _productService.Update(productDTO);
            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product is null)
                return BadRequest("This Product doesn't exist");

            await _productService.Delete(id);
            return Ok(product);
        }
    }
}
