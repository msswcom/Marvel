using Microsoft.Net.Http.Headers;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace HttpSender
{
    public class HttpService : IHttpService
    {
        HttpClient? http;

        public HttpService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public HttpClient Http
        {
            get
            {
                if (http == null)
                {
                    http = new HttpClient(new SocketsHttpHandler
                    {
                        PooledConnectionLifetime = TimeSpan.FromMinutes(15)
                    });
                }

                return http;
            }
        }

        public async Task<string> InvokeAsync(Task<HttpResponseMessage> task)
        {
            if (task != null)
            {
                task.Wait();

                var response = await task;

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var message = content;

                    if (String.IsNullOrEmpty(message))
                    {
                        message = response.StatusCode.ToString() + " " + response.ReasonPhrase;
                    }

                    throw new Exception(message);
                }

                return content;
            }

            return null;
        }

        public async Task<string> InvokeAsync<Input>(HttpMethod httpMethod, string url, Input? input,
            Dictionary<string, string>? headers,
            Dictionary<string, string>? cookies)
        {
            var message = new HttpRequestMessage(httpMethod, url);

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    message.Headers.Add(header.Key, header.Value);
                }
            }

            if (cookies != null && cookies.Count > 0)
            {
                message.Headers.Add(HeaderNames.Cookie, String.Join(";", cookies.Select(o => o.Key + "=" + o.Value)));
            }

            if (input != null)
            {
                if (input is string)
                {
                    message.Content = new StringContent(input as string,
                        Encoding.UTF8, MediaTypeNames.Application.Json);
                }
                else if (input is FormUrlEncodedContent)
                {
                    message.Content = input as FormUrlEncodedContent;
                }
                else
                {
                    message.Content = new StringContent(JsonSerializer.Serialize(input),
                        Encoding.UTF8, MediaTypeNames.Application.Json);
                }
            }

            var task = Http.SendAsync(message);

            return await InvokeAsync(task);
        }

        public async Task<string> GetAsync(string url,
            Dictionary<string, string>? headers = null,
            Dictionary<string, string>? cookies = null)
        {
            return await InvokeAsync<object>(HttpMethod.Get, url, null, headers, cookies);
        }

        public async Task<string> PostAsync<Input>(string url, Input? input,
            Dictionary<string, string>? headers = null,
            Dictionary<string, string>? cookies = null)
        {
            return await InvokeAsync<Input>(HttpMethod.Post, url, input, headers, cookies);
        }

        public async Task<string> PutAsync<Input>(string url, Input? input,
            Dictionary<string, string>? headers = null,
            Dictionary<string, string>? cookies = null)
        {
            return await InvokeAsync<Input>(HttpMethod.Put, url, input, headers, cookies);
        }

        public async Task<string> DeleteAsync(string url,
            Dictionary<string, string>? headers = null,
            Dictionary<string, string>? cookies = null)
        {
            return await InvokeAsync<object>(HttpMethod.Delete, url, null, headers, cookies);
        }
    }
}
