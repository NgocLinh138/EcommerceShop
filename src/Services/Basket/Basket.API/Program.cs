var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Add services to the container

// Carter
builder.Services.AddCarter();


// MediatR
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<, >));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


// Marten
builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("Database")!);
    option.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();


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

app.Run();
