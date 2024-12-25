using AutoMapper;
using Discount.API.Models;

namespace Discount.API.DTOs.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<CouponDTO, Coupon>().ReverseMap();
        }
    }
}
