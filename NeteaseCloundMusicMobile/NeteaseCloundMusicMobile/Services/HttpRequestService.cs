using Microsoft.AspNetCore.Components.WebAssembly.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Services
{

    /// <summary>
    /// 用来发起http请求
    /// </summary>
    public interface IHttpRequestService
    {
        Task<T> MakePostRequestAsync<T>(string url, object data = null, bool noCache = true);
        Task<string> MakePostRequestAsync(string url, object data = null, bool noCache = true);
    }
    class HttpRequestService : IHttpRequestService
    {
        private readonly HttpClient _httpClient;

        public HttpRequestService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<T> MakePostRequestAsync<T>(string url, object data = null, bool noCache = true)
        {

            return  Newtonsoft.Json.JsonConvert .DeserializeObject<T>(await MakePostRequestAsync(url, data, noCache));

        }

        public async Task<string> MakePostRequestAsync(string url, object data = null, bool noCache = true)
        {
            var query = Enumerable.Range(0, Convert.ToInt32(noCache))
              .Select(_ => KeyValuePair.Create("t", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()))
                 .Concat(
             data?.GetType().GetProperties().Select(x => KeyValuePair.Create(x.Name, x.GetValue(data)?.ToString())) ?? Array.Empty<KeyValuePair<string, string>>())
                 .ToArray();

            var queryString = string.Join("&", query.Select(x => $"{x.Key}={x.Value}"));
            url = string.Concat("/api",url, url.Contains("?") ? $"&{queryString}" : $"?{queryString}");
            //url = string.Concat(url, "&cookie=MUSIC_U=d5a4510625d1c8c3c571abdf9bae5a1b47e551ef2c37b3bcef99ffefe6bf9ecb0931c3a9fbfe3df2; __csrf=d2cecf80d31e9984f739105e82fa8817; NMTID=00OrZ7xwAJhZ8Vb30aDmthhX4AcU1cAAAF5k6U8tw");
            var httpClientRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url, UriKind.Relative),
            };
            httpClientRequestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            httpClientRequestMessage.SetBrowserRequestMode(BrowserRequestMode.Cors);
            httpClientRequestMessage.Headers.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            //httpClientRequestMessage.Headers.TryAddWithoutValidation("X-Requested-With", "HttpClient");
            var temp = await this._httpClient.SendAsync(httpClientRequestMessage);
            var json = await temp.Content.ReadAsStringAsync();
            
            return json;
        }
    }
}
