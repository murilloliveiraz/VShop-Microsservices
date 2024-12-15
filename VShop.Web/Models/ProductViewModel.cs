namespace VShop.Web.Models;

public class ProductViewModel
{
    public int id { get; set; }
    public string name { get; set; }
    public decimal price { get; set; }
    public string? description { get; set; }
    public long stock { get; set; }
    public string? imageurl { get; set; }
    public string? categoryName { get; set; }
    public int categoryid { get; set; }
}
