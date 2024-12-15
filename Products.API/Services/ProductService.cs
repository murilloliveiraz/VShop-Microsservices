using AutoMapper;
using Products.API.DTOs;
using Products.API.Models;
using Products.API.Repositories;

namespace Products.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductDTO> GetProductById(int id)
        {
            var product = await _repository.GetById(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await _repository.GetAll();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task Create(ProductDTO product)
        {
            var productCreated = _mapper.Map<Product>(product);
            await _repository.Create(productCreated);
            product.id = productCreated.id;
        }

        public async Task Delete(int id)
        {
            var product = _repository.GetById(id).Result;
            await _repository.Delete(product.id);
        }


        public async Task Update(ProductDTO product)
        {
            var productUpdated = _mapper.Map<Product>(product);
            await _repository.Update(productUpdated);
        }
    }
}
