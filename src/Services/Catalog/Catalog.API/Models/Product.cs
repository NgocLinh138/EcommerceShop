namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
