﻿namespace Catalog.API.Features.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}",
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteProductCommand(id));

                    var response = result.Adapt<DeleteProductResponse>();

                    return Results.Ok(response);
                })
            .WithName("Delete Product")
            .WithTags("Products")
            .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
