namespace Catalog.API.Features.Categories.GetCategoryById
{
    public record GetCategoryByIdResponse(Category Category);

    public class GetCategoryByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/categories/{categoryId}", 
                async (Guid categoryId, ISender sender) =>
                {
                    var result = await sender.Send(new GetCategoryByIdQuery(categoryId));   

                    var response = result.Adapt<GetCategoryByIdResponse>();

                    return Results.Ok(response);
                })
            .WithName("Get Category By Id")
            .WithTags("Categories")
            .Produces<GetCategoryByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Category By Id")
            .WithDescription("Get Category By Id");
        }
    }
}
