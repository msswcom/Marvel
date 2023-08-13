using Microsoft.Extensions.DependencyInjection;

namespace HttpSender
{
    public static class ServiceComposer
    {
        public static IServiceCollection AddHttpSender(this IServiceCollection services)
        {
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IExceptionHandler, ExceptionHandler>();

            return services;
        }
    }
}
