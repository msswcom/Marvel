using HttpSender;
using Marvel.Services.Characters;
using Marvel.Services.Pagination;
using Microsoft.Extensions.DependencyInjection;

namespace Marvel.Services
{
    public static class ServiceComposer
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpSender();
            services.AddTransient<IPaginationService, PaginationService>();
            services.AddTransient<IListService, ListService>();

            return services;
        }
    }
}
