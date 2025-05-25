// Add service to the container.

using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

//By this way we can add Carter related classes into our container.


//in here we can configure our mediator injection with specifying additional details(By this way we can register our mediator into current assembly.).
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
//configure Martin with our PostgreSQL connection string.specify the shopping cart entity will use the username property as its identity field.
//And lastly, use lightweight sessions to optimize performance by utilizing Martin's lightweight
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x=>x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

// Configure the http request pipeline .

//And this will be mapped Carter endpoints into our ASP.Net web API project.
app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
