using Discount.API.DTOs;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private ICouponRepository _repository;

        public CouponController(ICouponRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{couponCode}")]
        public async Task<ActionResult<CouponDTO>> GetDiscountCouponByCode(string couponCode)
        {
            var coupon = await _repository.GetCouponByCode(couponCode);

            if (coupon is null)
            {
                return NotFound($"Coupon Code: {couponCode} not found");
            }
            return Ok(coupon);
        }
    }
}
