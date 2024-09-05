namespace Catalog.API.Features.Categories.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : ICommand<DeleteCategoryResult>;

    public record DeleteCategoryResult(bool IsSuccess);

    public class DeleteCategoryCommandHandler
        (IDocumentSession session)
        : ICommandHandler<DeleteCategoryCommand, DeleteCategoryResult>
    {
        public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await session.LoadAsync<Category>(command.Id, cancellationToken);

            if (category is null)
            {
                throw new CategoryException.CategoryNotFoundException(command.Id);
            }

            session.Delete(category);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteCategoryResult(true);
        }
    }
}
