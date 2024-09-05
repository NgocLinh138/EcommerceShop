using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync()) return;

            var categories = GetPreconfiguredCategories();
            session.Store(categories.ToArray());

            var products = GetPreconfiguredProducts(categories);
            session.Store(products.ToArray());

            await session.SaveChangesAsync();
            Console.WriteLine("Data has been populated successfully.");
        }


        public static IEnumerable<Category> GetPreconfiguredCategories() => new List<Category>()
        {
            new Category { Id = Guid.NewGuid(), Name = "Tops", Description = "All types of tops." },
            new Category { Id = Guid.NewGuid(), Name = "Bottoms", Description = "All types of bottoms." },
            new Category { Id = Guid.NewGuid(), Name = "Outerwear", Description = "Jackets and coats." }
        };



        public static IEnumerable<Product> GetPreconfiguredProducts(IEnumerable<Category> categories)
        {
            var categoryList = categories.ToList();
            return new List<Product>()
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Basic T-Shirt",
                    Description = "A comfortable cotton t-shirt for everyday wear.",
                    ImageFile = "basic-tshirt.jpg",
                    Price = 150,
                    QuantityInStock = 100,
                    CategoryId = categoryList.FirstOrDefault(c => c.Name == "Tops")?.Id ?? Guid.Empty,
                    Category = categoryList.FirstOrDefault(c => c.Name == "Tops")
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Denim Jeans",
                    Description = "Classic blue denim jeans with a slim fit.",
                    ImageFile = "denim-jeans.jpg",
                    Price = 550,
                    QuantityInStock = 200,
                    CategoryId = categoryList.FirstOrDefault(c => c.Name == "Bottoms")?.Id ?? Guid.Empty,
                    Category = categoryList.FirstOrDefault(c => c.Name == "Bottoms")
                }
            };
        }
    }
}
