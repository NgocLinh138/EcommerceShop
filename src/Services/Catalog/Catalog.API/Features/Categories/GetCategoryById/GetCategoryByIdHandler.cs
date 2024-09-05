
namespace Catalog.API.Features.Categories.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IQuery<GetCategoryByIdResult>;

    public record GetCategoryByIdResult(Category Category);

    public class GetCategoryByIdCommandHandler
        (IDocumentSession session)
        : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdResult>
    {
        public async Task<GetCategoryByIdResult> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var category = await session.LoadAsync<Category>(query.Id, cancellationToken);

            if (category is null)
            {
                throw new CategoryException.CategoryNotFoundException(query.Id);
            }

            return new GetCategoryByIdResult(category);
        }
    }
}
