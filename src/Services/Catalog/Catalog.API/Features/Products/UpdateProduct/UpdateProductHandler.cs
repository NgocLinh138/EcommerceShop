namespace Catalog.API.Features.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, Guid CategoryId, string Description, string ImageFile, decimal Price, int QuantityInStock)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductException.ProductNotFoundException(command.Id);
            }

            product.Name = command.Name;
            product.CategoryId = command.CategoryId;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;
            product.QuantityInStock = command.QuantityInStock;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
