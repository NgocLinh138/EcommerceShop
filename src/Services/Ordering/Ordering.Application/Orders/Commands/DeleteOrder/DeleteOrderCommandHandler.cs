using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IApplicationDbContext context)
        : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.OrderId);

            var order = await context.Orders.FindAsync([orderId], cancellationToken: cancellationToken);

            if (order == null)
            {
                throw new OrderException.OrderNotFoundException(command.OrderId);
            }

            context.Orders.Remove(order);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}
