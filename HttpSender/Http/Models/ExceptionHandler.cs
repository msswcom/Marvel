using Marvel.Models.Pagination;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace HttpSender
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

            int code = 500;

            context.Response.StatusCode = code;
            context.Response.ContentType = "application/json";

            var message = "";
            
            while (exception != null)
            {
                message += (exception?.Message ?? typeof(Exception).Name) + " ";

                exception = exception.InnerException;
            }

            await context.Response.WriteAsync(JsonSerializer.Serialize(new Message
            {
                Code = code,
                Status = message
            }));
        }
    }
}
