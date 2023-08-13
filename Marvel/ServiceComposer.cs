using Marvel.Converters;

namespace Marvel
{
    public static class ServiceComposer
    {
        public static IServiceCollection AddConverters(this IServiceCollection services)
        {
            services.AddTransient<ICharactersComicsConverter, CharactersComicsConverter>();
            services.AddTransient<ICharactersConverter, CharactersConverter>();
            services.AddTransient<IComicsConverter, ComicsConverter>();

            return services;
        }
    }
}
