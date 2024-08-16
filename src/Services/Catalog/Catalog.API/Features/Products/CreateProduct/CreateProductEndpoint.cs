namespace Catalog.API.Features.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                async (CreateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateProductCommand>();
                    Console.WriteLine($"Sending command: {command}");
                    Console.WriteLine($"Sender: {sender}");

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateProductResponse>();

                    return Results.Created($"/products/{response.Id}", response);

                })
            .WithName("CreateProduct")
            .WithTags("Products")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }
    }
}
