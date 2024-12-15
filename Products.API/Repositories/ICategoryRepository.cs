using Products.API.Models;

namespace Products.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetCategorieProducts();
        Task<Category> GetById(int id);
        Task Create(Category category);
        Task Update(Category category);
        Task Delete(int id);
    }
}
