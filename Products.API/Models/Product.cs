namespace Products.API.Models;

public class Product
{
    public int id { get; set; }
    public string? name { get; set; }
    public decimal  price { get; set; }
    public string? description { get; set; }
    public long stock { get; set; }
    public string? imageurl { get; set; }
    public Category? category { get; set; }
    public int categoryid { get; set; }
}
