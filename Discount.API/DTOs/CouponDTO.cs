﻿using System.ComponentModel.DataAnnotations;

namespace Discount.API.DTOs
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        [Required]
        public string? CouponCode { get; set; }
        [Required]
        public decimal Discount { get; set; }
    }
}
