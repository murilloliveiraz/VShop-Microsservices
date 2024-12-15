using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Products.API.DTOs;
using Products.API.Models;
using Products.API.Services;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get() 
        {
            var categories = await _categoryService.GetCategories();
            if(categories is null)
                return NotFound("Categories not found");
            return Ok(categories);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            var categories = await _categoryService.GetCategorieProducts();
            if (categories is null)
                return NotFound("Categories not found");
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category is null)
                return NotFound("Category not found");
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                return BadRequest("Category can't be null");

            await _categoryService.Create(categoryDTO);
            return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.categoryid }, categoryDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id !=  categoryDTO.categoryid)
                return BadRequest("The id informed and the Id of the Category are not the same");

            if (categoryDTO is null)
                return BadRequest("Category can't be null");

            await _categoryService.Update(categoryDTO);
            return Ok(categoryDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category is null)
                return BadRequest("This Category doesn't exist");

            await _categoryService.Delete(id);
            return Ok(category);
        }
    }
}
