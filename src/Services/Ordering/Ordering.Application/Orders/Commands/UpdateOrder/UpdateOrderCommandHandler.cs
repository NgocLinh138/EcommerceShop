using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler(IApplicationDbContext context)
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);

            var order = await context.Orders.FindAsync([orderId], cancellationToken: cancellationToken);

            if (order is null)
            {
                throw new OrderException.OrderNotFoundException(command.Order.Id);
            }

            UpdateOrder(order, command.Order);

            context.Orders.Update(order);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        public void UpdateOrder(Order order, OrderDto orderDto)
        {
            var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.City, orderDto.ShippingAddress.ZipCode);
            var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.City, orderDto.BillingAddress.ZipCode);
            var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            order.Update(
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: updatedShippingAddress,
                billingAddress: updatedBillingAddress,
                payment: updatedPayment,
                orderStatus: orderDto.Status);
        }
    }
}
