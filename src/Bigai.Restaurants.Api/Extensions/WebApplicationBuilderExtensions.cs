using Bigai.Restaurants.Api.Middlewares;
using Bigai.Restaurants.Application.Ioc;
using Bigai.Restaurants.Infrastructure.IoC;

using Microsoft.OpenApi.Models;

using Serilog;

namespace Bigai.Restaurants.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddInfrastructure(builder.Configuration)
                        .AddApplication();

        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration)
        );

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            []
        }
            });
        });

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
    }
}