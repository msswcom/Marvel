using HttpSender;
using Marvel.Models.Extensions;
using Marvel.Models.Settings;
using Marvel.Services.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Marvel.Services.Comics
{
    public class ComicsPaginationService : IComicsPaginationService
    {
        public const string Service = "/comics";
        private readonly IHttpService httpService;
        private readonly ServicesSettings? servicesSettings;

        public ComicsPaginationService(IHttpService httpService, IOptions<Settings> settings)
        {
            this.httpService = httpService;
            servicesSettings = settings?.Value?.Services;

            if (servicesSettings == null
                || String.IsNullOrEmpty(servicesSettings.Url)
                || String.IsNullOrEmpty(servicesSettings.PublicKey)
                || String.IsNullOrEmpty(servicesSettings.PrivateKey))
            {
                throw new ArgumentNullException(nameof(servicesSettings));
            }
        }

        public async Task<MarvelMessageData<MarvelComic>> PaginationItemsAsync(int offset = 0, int limit = MarvelPagination.Limit)
        {
            var url = servicesSettings?.Url + Service;

            var ts = DateTime.Now.ToString2();

            var parameters = new Dictionary<string, string>
            {
                { "apikey", servicesSettings?.PublicKey},
                { "ts", ts },
                { "hash", (ts + servicesSettings.PrivateKey + servicesSettings.PublicKey).Md5() },
                { "offset", offset.ToString() },
                { "limit", limit.ToString() },
                { "orderBy", "title" }
            };

            url = QueryHelpers.AddQueryString(url, parameters);

            var response = "";

            int count = 3;

            for (int i = 0; i < count; ++i)
            {
                try
                {
                    response = await httpService.GetAsync(url);

                    break;
                }
                catch
                {
                    if (i == count - 1)
                    {
                        throw;
                    }
                }
            }

            if (!String.IsNullOrEmpty(response))
            {
                var data = JsonSerializer.Deserialize<MarvelMessageData<MarvelComic>>(response);

                return data;
            }
            else
            {
                throw new Exception($"Marvel service {Service} returned empty response");
            }
        }
    }
}
