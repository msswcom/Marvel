using Marvel.Database.Services.Characters;
using Marvel.Database.Services.CharactersComics;
using Marvel.Database.Services.Comics;
using Marvel.Database.Services.DownloadLogs;
using Microsoft.Extensions.DependencyInjection;

namespace Marvel.Database
{
    public static class ServiceComposer
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddTransient<IDatabaseCharactersComics, DatabaseCharactersComics>();
            services.AddTransient<IDatabaseCharacters, DatabaseCharacters>();
            services.AddTransient<IDatabaseComics, DatabaseComics>();
            services.AddTransient<IDatabaseDownloadLog, DatabaseDownloadLog>();

            return services;
        }
    }
}
