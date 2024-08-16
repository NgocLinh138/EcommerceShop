using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync()) return;

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        public static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
        {
            new Product()
            {
                Id = new Guid("249DAF5F-B742-4F49-88D5-8F55D8A484C6"),
                Name = "Basic T-Shirt",
                Description = "A comfortable cotton t-shirt for everyday wear.",
                ImageFile = "basic-tshirt.jpg",
                Price = 150,
                Category = new List<string> { "Tops", "Casual" }
            },
            new Product()
            {
                Id = new Guid("B64E7A34-AF8D-4A56-917C-6F49D8D0BB51"),
                Name = "Denim Jeans",
                Description = "Classic blue denim jeans with a slim fit.",
                ImageFile = "denim-jeans.jpg",
                Price = 550,
                Category = new List<string> { "Bottoms", "Casual" }
            },
            new Product()
            {
                Id = new Guid("5D3545D4-939A-4BC1-9C2D-098C1C6A84A5"),
                Name = "Hooded Sweatshirt",
                Description = "A cozy hoodie perfect for cool weather.",
                ImageFile = "hooded-sweatshirt.jpg",
                Price = 350,
                Category = new List<string> { "Outerwear", "Athleisure" }
            },
            new Product()
            {
                Id = new Guid("7B2F4E47-1FC5-4C6F-8E47-B6B96AB2E844"),
                Name = "Summer Dress",
                Description = "A light and flowy summer dress with floral patterns.",
                ImageFile = "summer-dress.jpg",
                Price = 300,
                Category = new List<string> { "Dresses", "Casual" }
            },
            new Product()
            {
                Id = new Guid("F9F4DA25-2B19-4892-9C69-F3AB504756AA"),
                Name = "Leather Jacket",
                Description = "A stylish leather jacket for a bold look.",
                ImageFile = "leather-jacket.jpg",
                Price = 1500,
                Category = new List<string> { "Outerwear", "Fashion" }
            },
            new Product()
            {
                Id = new Guid("3A1D39E9-1B52-4C6E-9E43-C1C2C04A12D6"),
                Name = "Running Shoes",
                Description = "Lightweight running shoes with excellent grip.",
                ImageFile = "running-shoes.jpg",
                Price = 750,
                Category = new List<string> { "Footwear", "Athletic" }
            },
            new Product()
            {
                Id = new Guid("9A3D75F3-CEFB-41ED-A3A6-50C7A75CE848"),
                Name = "Polo Shirt",
                Description = "A smart casual polo shirt with a slim fit.",
                ImageFile = "polo-shirt.jpg",
                Price = 250,
                Category = new List<string> { "Tops", "Smart Casual" }
            },
            new Product()
            {
                Id = new Guid("87C1E6C5-0D27-4AFC-BB89-2A50B75DA627"),
                Name = "Cargo Pants",
                Description = "Durable cargo pants with multiple pockets.",
                ImageFile = "cargo-pants.jpg",
                Price = 450,
                Category = new List<string> { "Bottoms", "Utility" }
            },
            new Product()
            {
                Id = new Guid("634C7D25-2FD7-478F-93A8-017527B30C35"),
                Name = "Sports Jacket",
                Description = "Lightweight jacket designed for outdoor activities.",
                ImageFile = "sports-jacket.jpg",
                Price = 950,
                Category = new List<string> { "Outerwear", "Sport" }
            },
            new Product()
            {
                Id = new Guid("5F78D204-743E-41BB-A7E4-EC537AF9C5D6"),
                Name = "Wool Scarf",
                Description = "A warm wool scarf for winter days.",
                ImageFile = "wool-scarf.jpg",
                Price = 200,
                Category = new List<string> { "Accessories", "Winter" }
            },

        };
    }
}
