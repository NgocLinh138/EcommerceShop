namespace Catalog.API.Features.Categories.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, string Name, string? Description)
        : ICommand<UpdateCategoryResult>;

    public record UpdateCategoryResult(bool IsSuccess);

    public class UpdateCategoryCommandHandler
        (IDocumentSession session)
        : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResult>
    {
        public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await session.LoadAsync<Category>(command.Id, cancellationToken);
            
            if (category is null)
            {
                throw new CategoryException.CategoryNotFoundException(command.Id);
            }

            category.Name = command.Name;   
            category.Description = command.Description;

            session.Update(category);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateCategoryResult(true);
        }
    }
}
