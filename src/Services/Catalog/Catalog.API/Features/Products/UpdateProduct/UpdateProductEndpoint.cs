namespace Catalog.API.Features.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products",
                async (UpdateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateProductCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateProductResponse>();

                    return Results.Ok(response);
                })
            .WithName("Update Product")
            .WithTags("Products")
            .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Updates an existing product by ID.")
            .WithDescription("This endpoint allows updating the details of an existing product, including its name, categories, description, image, and price.");
        }
    }
}
