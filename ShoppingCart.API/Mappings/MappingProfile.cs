using AutoMapper;
using ShoppingCart.API.DTOs;
using ShoppingCart.API.Models;

namespace ShoppingCart.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CartDTO, Cart>().ReverseMap();
            CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();
            CreateMap<CartItemDTO, CartItem>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
        }
    }
}
