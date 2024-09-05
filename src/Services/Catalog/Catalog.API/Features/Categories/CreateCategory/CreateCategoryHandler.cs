namespace Catalog.API.Features.Categories.CreateCategory
{
    public record CreateCategoryCommand(string Name, string? Description) : ICommand<CreateCategoryResult>;
    public record CreateCategoryResult(Category Category);

    public class CreateCategoryCommandHandler
        (IDocumentSession session)
        : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
    {
        public async Task<CreateCategoryResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = command.Name,
                Description = command.Description
            };

            session.Store(category);
            await session.SaveChangesAsync();

            return new CreateCategoryResult(category);
        }
    }
}
