using Bookify.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace Bookify.Middleware
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly ITransientService _transient;
        private readonly ISingletonService _singleton;
        private readonly ILogger<Middleware> _logger;

        //private readonly IScopedService _scoped;

        public Middleware(RequestDelegate next, ITransientService transient, ISingletonService singleton,
            ILogger<Middleware> logger)
        {
            _next = next;
            _transient = transient;
            _singleton = singleton;
            _logger = logger;
            _logger.LogInformation($"Middleware Singleton GUID: {_singleton.Guid}");
            _logger.LogInformation($"Middleware Transient GUID: {_transient.Guid}");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next.Invoke(httpContext);

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
