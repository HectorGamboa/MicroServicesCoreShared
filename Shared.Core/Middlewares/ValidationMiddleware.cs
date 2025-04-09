using Microsoft.AspNetCore.Http;
using Shared.Core.Commons.Bases;
using Shared.Core.Commons.Exceptions;
using System.Text.Json;

namespace Shared.Core.Middlewares
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)  // Manejo de errores de validación (400 Bad Request)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await JsonSerializer.SerializeAsync(context.Response.Body, new BaseResponse<object>
                {
                    IsSuccess = false,
                    Message = "Validation error",
                    Errors = ex.Errors,
                });
            }
        }
    }
}
