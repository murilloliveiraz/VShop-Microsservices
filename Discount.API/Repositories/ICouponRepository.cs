using Discount.API.DTOs;

namespace Discount.API.Repositories
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponByCode(string couponCode);
    }
}
