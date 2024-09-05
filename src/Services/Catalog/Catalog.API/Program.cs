using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Add services to the container

// MediatR
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


// Validator
builder.Services.AddValidatorsFromAssembly(assembly);


// Carter
builder.Services.AddCarter();


// Marten
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.AutoCreateSchemaObjects = AutoCreate.All;
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}


// ExceptionHandler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();


// HealthCheck
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);


// Swagger
builder.Services.AddEndpointsApiExplorer();  // Required for Swagger with minimal APIs
builder.Services.AddSwaggerGen(); // Add Swagger

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
