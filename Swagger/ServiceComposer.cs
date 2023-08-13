using Microsoft.Extensions.DependencyInjection;

namespace Swagger
{
    public static class ServiceComposer
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
