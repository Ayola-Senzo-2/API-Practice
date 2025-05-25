using System.Diagnostics;

namespace KasiCornerKota_API.Middleware
{
    public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate request)
        {
            var stopWatch = Stopwatch.StartNew();
            await request.Invoke(context);
            stopWatch.Stop();

            if (stopWatch.ElapsedMilliseconds / 3000 > 4)
            {
                logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms",
                    context.Request.Method,
                    context.Request.Path,
                    stopWatch.ElapsedMilliseconds);
            }
        }
    }
}
