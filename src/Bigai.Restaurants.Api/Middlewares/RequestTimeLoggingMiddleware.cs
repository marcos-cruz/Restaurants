using System.Diagnostics;

namespace Bigai.Restaurants.Api.Middlewares;

public class RequestTimeLoggingMiddleware : IMiddleware
{
    private readonly ILogger<RequestTimeLoggingMiddleware> _logger;

    public RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        const long FOUR_SECONDS = 4;

        var stopWatch = Stopwatch.StartNew();

        await next.Invoke(context);

        stopWatch.Stop();

        long elapsedTimeInSeconds = stopWatch.ElapsedMilliseconds / 1000;

        if (elapsedTimeInSeconds > FOUR_SECONDS)
        {
            _logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms",
                                   context.Request.Method,
                                   context.Request.Path,
                                   stopWatch.ElapsedMilliseconds);

            context.Response.StatusCode = 500;

            await context.Response.WriteAsync("Something went wrong");
        }
    }
}