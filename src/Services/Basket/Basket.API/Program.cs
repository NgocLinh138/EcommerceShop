using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Add services to the container


// Application Services
// Carter
builder.Services.AddCarter();

// MediatR
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<, >));
    config.AddOpenBehavior(typeof(LoggingBehavior<, >));
});



// Data Services
// Marten
builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("Database")!);
    option.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

// Swagger
builder.Services.AddEndpointsApiExplorer();  // Required for Swagger with minimal APIs
builder.Services.AddSwaggerGen(); // Add Swagger

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

// Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});



// Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});




// Cross-cutting Services
// Exception
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Health
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI to be served at the app's root (http://localhost:<port>)
});
app.UseRouting();

// Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
