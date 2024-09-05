namespace Catalog.API.Features.Categories.DeleteCategory
{
    public class DeleteCategoryCommandValidation : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Category Id is required.");
        }
    }
}
