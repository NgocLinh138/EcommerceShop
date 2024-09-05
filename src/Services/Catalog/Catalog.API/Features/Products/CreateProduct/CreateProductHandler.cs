using Microsoft.AspNetCore.Http;

namespace Catalog.API.Features.Products.CreateProduct
{
    public record CreateProductCommand(string Name, Guid CategoryId, string Description, string ImageFile, decimal Price, int QuantityInStock)
        : ICommand<CreateProductResult>;


    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var category = await session.LoadAsync<Category>(command.CategoryId, cancellationToken);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            var product = new Product
            {
                Name = command.Name,
                CategoryId = command.CategoryId,
                Category = category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                QuantityInStock = command.QuantityInStock
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
