using HttpSender;
using Marvel.Services.Characters;
using Marvel.Services.CharactersByComic;
using Marvel.Services.Comics;
using Microsoft.Extensions.DependencyInjection;

namespace Marvel.Services
{
    public static class ServiceComposer
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpSender();
            services.AddTransient<ICharactersByComicPaginationService, CharactersByComicPaginationService>();
            services.AddTransient<ICharactersPaginationService, CharactersPaginationService>();
            services.AddTransient<IComicsPaginationService, ComicsPaginationService>();
            services.AddTransient<ICharactersByComicService, CharactersByComicService>();
            services.AddTransient<ICharactersService, CharactersService>();
            services.AddTransient<IComicsService, ComicsService>();

            return services;
        }
    }
}
