namespace Catalog.API.Features.Categories.CreateCategory {
    public record CreateCategoryRequest(string Name, string? Description);
    public record CreateCategoryResponse(Category Category);

    public class CreateCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/categories", 
                async (CreateCategoryRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateCategoryCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateCategoryResponse>();

                    return Results.Created($"/categories/{response.Category.Id}", response);
                })
            .WithName("Create Category")
            .WithTags("Categories")
            .Produces<CreateCategoryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Category")
            .WithDescription("Create Category");
        }
    }
}
