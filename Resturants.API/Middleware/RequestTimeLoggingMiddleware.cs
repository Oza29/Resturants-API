
using Azure.Core;
using System.Diagnostics;

namespace Resturants.API.Middleware
{
    public class RequestTimeLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeLoggingMiddleware> _logger;
        public RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await next.Invoke(context);
            stopWatch.Stop();
            //endpoint takes more than 4 seconds to handle request
            if (stopWatch.ElapsedMilliseconds / 1000 > 4)
            {
                _logger.LogInformation($"Request {context.Request.Method} at path {context.Request.Path} " +
                    $"took {stopWatch.ElapsedMilliseconds} ms");
            }

        }
    }
}
