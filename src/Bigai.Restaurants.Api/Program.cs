using Bigai.Restaurants.Infrastructure.Seeders;
using Serilog;
using Bigai.Restaurants.Api.Middlewares;
using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddPresentation();


var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

await seeder.Seed();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("api/identity")
   .WithTags("Identity")
   .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
