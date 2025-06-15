var builder = WebApplication.CreateBuilder(args);

//add services to the container.

//------------------------------
//Infrastructure - ef core 
// application - mediatR
//api- carter - health check.....

//builder.Services
//    .AddApplicationServices()
//    .AddInfrastructureServices(builder.Configuration)
//    .AddApiServices();
var app = builder.Build();

// Configure the http request pipeline.

app.Run();
