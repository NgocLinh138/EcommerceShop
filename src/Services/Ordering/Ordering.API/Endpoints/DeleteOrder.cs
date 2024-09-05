using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{
    //public record DeleteOrderRequest();
    public record DeleteOrderResponse(bool IsSuccess);

    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id}", async(Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteOrderCommand(id));

                var response = result.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            })
            .WithName("Delete order")
            .Produces<DeleteOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete order")
            .WithDescription("Delete order");
        }
    }
}
