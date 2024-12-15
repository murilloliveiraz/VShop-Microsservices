using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VShop.Web.Models;

public class ProductViewModel
{
    public int id { get; set; }
    [Required]
    public string? name { get; set; }
    [Required]
    public decimal price { get; set; }
    [Required]
    public string? description { get; set; }
    [Required]
    public long stock { get; set; }
    [Required]
    public string? imageurl { get; set; }
    public string? categoryName { get; set; }
    [Display(Name = "Categorias")]
    public int categoryid { get; set; }
}
