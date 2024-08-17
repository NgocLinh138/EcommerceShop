﻿namespace Basket.API.Features.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);

    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", 
                async (StoreBasketRequest request, ISender sender) =>
                {
                    var command = request.Adapt<StoreBasketRequest>();  

                    var result = await sender.Send(command);    

                    var response = result.Adapt<StoreBasketResponse>();

                    return Results.Created($"/basket/{response.UserName}", response);
                })
            .WithName("CreateProduct")
            .WithTags("Basket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }
    }
}
