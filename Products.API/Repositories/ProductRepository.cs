using Microsoft.EntityFrameworkCore;
using Products.API.Contexts;
using Products.API.Models;

namespace Products.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.products.Include(c => c.category).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.products.Include(c => c.category).Where(p => p.id == id)
            .FirstOrDefaultAsync();
        }
        public async Task Create(Product product)
        {
            _context.products.Add(product);
        }

        public async Task Delete(int id)
        {
            var product = await GetById(id);
            _context.products.Remove(product);
        }

        public async Task Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }
    }
}
