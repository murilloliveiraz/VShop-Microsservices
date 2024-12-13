﻿using Products.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Products.API.DTOs
{
    public class ProductDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string name { get; set; }
        [Required(ErrorMessage = "The Price is Required")]
        public decimal price { get; set; }
        [Required(ErrorMessage = "The Description is Required")]
        public string description { get; set; }
        [Required(ErrorMessage = "The Stock is Required")]
        [MinLength(1)]
        [MaxLength(9999)]
        public long stock { get; set; }
        public string imageurl { get; set; }
        public Category category { get; set; }
        public int categoryid { get; set; }
    }
}