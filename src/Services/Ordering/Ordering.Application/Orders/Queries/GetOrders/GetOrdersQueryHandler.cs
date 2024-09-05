
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await context.Orders.LongCountAsync(cancellationToken);

            var orders = await context.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .OrderBy(x => x.OrderName.Value)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(
                new PaginatedResult<OrderDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    orders.ToOrderDtoList()
                    ));
        }
    }
}
