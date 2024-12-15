using System.ComponentModel.DataAnnotations;

namespace Products.API.Models;

public class Category
{
    public int categoryid { get; set; }
    public string? name { get; set; }
    public ICollection<Product> products { get; set; }
}
