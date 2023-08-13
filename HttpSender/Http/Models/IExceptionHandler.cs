using Microsoft.AspNetCore.Http;

namespace HttpSender
{
    public interface IExceptionHandler
    {
        public Task InvokeAsync(HttpContext context);
    }
}
