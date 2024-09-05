namespace Catalog.API.Features.Categories.UpdateCategory
{
    public record UpdateCategoryRequest(Guid Id, string Name, string? Description);
    public record UpdateCategoryResponse(bool IsSuccess);

    public class UpdateCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/categories",
                async (UpdateCategoryRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateCategoryCommand>();   

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateCategoryResponse>();

                    return Results.Ok(response);
                })
            .WithName("Update Category")
            .WithTags("Categories")
            .Produces<UpdateCategoryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Updates an existing category by ID.")
            .WithDescription("This endpoint allows updating the details of an existing category, including its name, description.");
        }
    }
}
