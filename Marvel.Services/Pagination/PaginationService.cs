using HttpSender;
using Marvel.Models.Extensions;
using Marvel.Models.Settings;
using Marvel.Services.Models;
using Marvel.Services.Pagination;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Marvel.Services.Characters
{
    public class PaginationService : IPaginationService
    {
        private readonly IHttpService httpService;
        private readonly ServicesSettings? servicesSettings;

        public PaginationService(IHttpService httpService, IOptions<Settings> settings)
        {
            this.httpService = httpService;
            servicesSettings = settings?.Value?.Services;

            if (String.IsNullOrEmpty(servicesSettings?.PublicKey) || servicesSettings?.PublicKey == "PublicKey")
            {
                throw new Exception("PublicKey should be set in appsettings.json.");
            }
            if (String.IsNullOrEmpty(servicesSettings?.PrivateKey) || servicesSettings?.PrivateKey == "PrivateKey")
            {
                throw new Exception("PrivateKey should be set in appsettings.json.");
            }
        }

        public async Task<MarvelMessageData<T>> ToPaginationItemsAsync<T>(
            string url,
            string sort,
            int offset = 0,
            int limit = MarvelPagination.Limit)
        {
            url = servicesSettings?.Url + url;

            var ts = DateTime.Now.ToString2();

            var parameters = new Dictionary<string, string>
            {
                { "apikey", servicesSettings?.PublicKey ?? ""},
                { "ts", ts },
                { "hash", (ts + servicesSettings?.PrivateKey + servicesSettings?.PublicKey).Md5() },
                { "offset", offset.ToString() },
                { "limit", limit.ToString() },
                { "orderBy", sort }
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
                var data = JsonSerializer.Deserialize<MarvelMessageData<T>>(response);

                if (data == null)
                {
                    throw new Exception("MarvelMessageData null");
                }
                else
                {
                    return data;
                }
            }
            else
            {
                throw new Exception($"Marvel service {url} returned empty response");
            }
        }
    }
}
