namespace Catalog.API.Features.Categories.DeleteCategory
{
    public record DeleteCategoryResponse(bool IsSuccess);

    public class DeleteCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/categories/{categoryId}", 
                async (Guid categoryId, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteCategoryCommand(categoryId));

                    var response = result.Adapt<DeleteCategoryResponse>();

                    return Results.Ok(response);
                })
            .WithName("Delete Category")
            .WithTags("Categories")
            .Produces<DeleteCategoryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Category")
            .WithDescription("Delete Category");
        }
    }
}
