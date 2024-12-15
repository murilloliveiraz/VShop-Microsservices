using AutoMapper;
using Products.API.DTOs;
using Products.API.Models;

namespace Products.API.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.categoryName, opt => opt.MapFrom(src => src.category.name));
        }
    }
}
