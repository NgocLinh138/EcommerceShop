namespace Catalog.API.Features.Products.UpdateProduct
{
    public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
            RuleFor(x => x.Name)
                          .NotEmpty().WithMessage("Product Name is required.")
                          .Length(2, 200).WithMessage("Product Name must be between 2 - 200 characters."); RuleFor(x => x.Category).NotEmpty().WithMessage("Product Category is required.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Product Category is required.");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Product ImageFile is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product Price must be greater than 0.");
        }
    }
}
