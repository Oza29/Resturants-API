
using Domain.Exceptions;
using System.Runtime.InteropServices;

namespace Resturants.API.Middleware
{
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
            catch(NotFoundException notfound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfound.Message);
                _logger.LogWarning(notfound.Message);
            }
            catch(ForbiddenException forbidden)
            {
                context.Response.StatusCode = 403;
                await context .Response.WriteAsync("Access Forbidden!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
            
        }
    }
}
