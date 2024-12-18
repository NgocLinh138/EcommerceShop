﻿namespace Catalog.API.Features.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(Guid CategoryId) : IQuery<GetProductsByCategoryResult>;

    public record GetProductsByCategoryResult(IEnumerable<Product> Products);

    public class GetProductsByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .Where(x => x.CategoryId == query.CategoryId)
                .ToListAsync(cancellationToken);

            return new GetProductsByCategoryResult(products);
        }
    }
}
