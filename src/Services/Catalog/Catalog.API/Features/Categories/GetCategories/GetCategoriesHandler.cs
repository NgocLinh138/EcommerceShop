namespace Catalog.API.Features.Categories.GetCategories
{
    public record GetCategoriesQuery(int? PageIndex = 1, int? PageSize = 10) : IQuery<GetCategoriesResult>;
    public record GetCategoriesResult(IEnumerable<Category> Categories);

    public class GetCategoriesHandler
        (IDocumentSession session)
        : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
    {
        public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            var categories = await session.Query<Category>()
                .ToPagedListAsync(query.PageIndex ?? 1, query.PageSize ?? 10, cancellationToken);

            return new GetCategoriesResult(categories);
        }
    }
}
