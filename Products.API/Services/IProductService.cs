using Products.API.DTOs;

namespace Products.API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int id);
        Task Create(ProductDTO product);
        Task Update(ProductDTO product);
        Task Delete(int id);
    }
}
