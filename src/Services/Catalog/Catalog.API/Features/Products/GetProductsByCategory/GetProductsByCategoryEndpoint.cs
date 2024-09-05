namespace Catalog.API.Features.Products.GetProductsByCategory
{
    public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{categoryId:guid}",
                async (Guid categoryId, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductsByCategoryQuery(categoryId));

                    var response = result.Adapt<GetProductsByCategoryResponse>();

                    return Results.Ok(response);
                })
            .WithName("Get Products By Category")
            .WithTags("Products")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Category")
            .WithDescription("Get Products By Category");
        }
    }
}
