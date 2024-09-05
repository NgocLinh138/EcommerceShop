namespace Catalog.API.Features.Categories.CreateCategory
{
    public class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category Name is required.")
                .Length(2, 200).WithMessage("Category Name must be between 2 - 200 characters.");
        }
    }
}
