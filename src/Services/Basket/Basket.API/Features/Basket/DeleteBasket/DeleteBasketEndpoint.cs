
using Basket.API.Features.Basket.GetBasket;

namespace Basket.API.Features.Basket.DeleteBasket
{
    public record DeleteBasketResponse(bool IsSuccess);

    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{username}",
                async (string username, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteBasketCommand(username));

                    var response = result.Adapt<DeleteBasketResponse>();

                    return Results.Ok(response);
                })
            .WithName("DeleteProduct")
            .WithTags("Basket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
