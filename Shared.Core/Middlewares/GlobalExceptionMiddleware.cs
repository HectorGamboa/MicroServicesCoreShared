using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Core.Middlewares
{
    public class GlobalExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            string message = "Internal Server Error, try again";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Error";
            try
            {
                await next(context);
                //validamos si respondemos Límites de Rate-Limiting  tráfico excesivo
                if (context.Response.StatusCode == 429)
                {
                    message = "Too many requests";
                    statusCode = (int)HttpStatusCode.TooManyRequests;
                    title = "Warining";
                    await ModifyHeader(context, title, message, statusCode);
                }
                //validamos si respondemos  401 Unauthorized
                if (context.Response.StatusCode == 401)
                {
                    message = "You  are not authorizated to access";
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    title = "Alert";
                    await ModifyHeader(context, title, message, statusCode);
                }
                //validamos si respondemos  403 Forbidden
                if (context.Response.StatusCode == 403)
                {
                    message = "You are not allowed to access";
                    statusCode = (int)HttpStatusCode.Forbidden;
                    title = "Alert";
                    await ModifyHeader(context, title, message, statusCode);
                }
            }
            catch (Exception ex)
            {
                //log original exception /File , Console, Debugger
                LogException.LogExceptions(ex);

                // es timeout
                if (ex is TaskCanceledException || ex is TimeoutException)
                {
                    message = "Request time out ... try again";
                    statusCode = (int)HttpStatusCode.RequestTimeout;
                    title = "Out of time";
                    await ModifyHeader(context, title, message, statusCode);
                }

                //Errores por default
                await ModifyHeader(context, title, message, statusCode);

            }
        }

        private async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
        {
            // display the message in the header
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
            {
                Status = statusCode,
                Detail = message,
                Title = title
            }), CancellationToken.None);
        }
    }
}
