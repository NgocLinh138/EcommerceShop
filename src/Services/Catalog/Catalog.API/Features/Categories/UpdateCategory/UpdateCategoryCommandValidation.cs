namespace Catalog.API.Features.Categories.UpdateCategory
{
    public class UpdateCategoryCommandValidation : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Category Id is required.");
            RuleFor(x => x.Name)
                          .NotEmpty().WithMessage("Category Name is required.")
                          .Length(2, 200).WithMessage("Category Name must be between 2 - 200 characters.");
        }
    }
}
