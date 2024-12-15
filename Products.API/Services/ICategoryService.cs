using Products.API.DTOs;

namespace Products.API.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<IEnumerable<CategoryDTO>> GetCategorieProducts();
        Task<CategoryDTO> GetCategoryById(int id);
        Task Create(CategoryDTO category);
        Task Update(CategoryDTO category);
        Task Delete(int id);
    }
}
