using AutoMapper;
using Products.API.DTOs;
using Products.API.Models;
using Products.API.Repositories;

namespace Products.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategorieProducts()
        {
            var categories = await _repository.GetCategorieProducts();
            return _mapper.Map<ICollection<CategoryDTO>>(categories);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = await _repository.GetAll();
            return _mapper.Map<ICollection<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var category = await _repository.GetById(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task Create(CategoryDTO category)
        {
            var categoryCreated = _mapper.Map<Category>(category);
            await _repository.Create(categoryCreated);
            category.categoryid = categoryCreated.categoryid;
        }

        public async Task Delete(int id)
        {
            var category = _repository.GetById(id).Result;
            await _repository.Delete(category.categoryid);
        }

        public async Task Update(CategoryDTO category)
        {
            var categoryUpdated = _mapper.Map<Category>(category);
            await _repository.Update(categoryUpdated);
        }
    }
}
