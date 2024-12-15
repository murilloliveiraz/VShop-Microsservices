using Products.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Products.API.DTOs
{
    public class CategoryDTO
    {
        public int categoryid { get; set; }
        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? name { get; set; }
        public ICollection<Product>? products { get; set; }
    }
}
