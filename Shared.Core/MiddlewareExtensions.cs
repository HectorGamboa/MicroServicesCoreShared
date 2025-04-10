using Microsoft.AspNetCore.Builder;
using Shared.Core.Middlewares;

namespace Shared.Core
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder AddMiddleware(this IApplicationBuilder builder1)
        {
            builder1.UseMiddleware<ValidationMiddleware>();
            builder1.UseMiddleware<GlobalExceptionMiddleware>();
            return builder1;
        }
    }
}
