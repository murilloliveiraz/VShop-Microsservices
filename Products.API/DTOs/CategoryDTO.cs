namespace Products.API.DTOs
{
    public class CategoryDTO
    {
        public int categoryid { get; set; }
        public string? name { get; set; }
        public ICollection<Product> products { get; set; }
    }
}
