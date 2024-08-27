using Bigai.Restaurants.Domain.Exceptions;

namespace Bigai.Restaurants.Api.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFound.Message);

            _logger.LogWarning(notFound, notFound.Message);
        }
        catch (ForbidException ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 403;

            await context.Response.WriteAsync("Access forbidden");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;

            await context.Response.WriteAsync("Something went wrong");
        }
    }
}