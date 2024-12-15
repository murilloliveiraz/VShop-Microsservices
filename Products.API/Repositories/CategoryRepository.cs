using Microsoft.EntityFrameworkCore;
using Products.API.Contexts;
using Products.API.Models;

namespace Products.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _context;
        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.categories.Include(c => c.products).ToListAsync();
        }
        public async Task<Category> GetById(int id)
        {
            return await _context.categories.Include(c => c.products).FirstOrDefaultAsync(c => c.categoryid == id);
        }
        public async Task<IEnumerable<Category>> GetCategorieProducts()
        {
            return await _context.categories.Include(c => c.products).ToListAsync();
        }
        public async Task Create(Category category)
        {
            await _context.categories.AddAsync(category);
        }
        public async Task Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            var category = await GetById(id);
            _context.categories.Remove(category);
        }
    }
}
