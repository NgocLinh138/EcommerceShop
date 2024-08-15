namespace Catalog.API.Features.Products.DeleteProduct
{
    public class DeleteProductCommandValidation : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
        }
    }
}
