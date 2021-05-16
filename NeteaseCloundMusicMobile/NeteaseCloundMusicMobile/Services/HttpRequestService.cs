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

    }
    class HttpRequestService : IHttpRequestService
    {
        private readonly HttpClient _httpClient;

        public HttpRequestService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        private async Task<T> RequestImplAsync<T>(string url)
        {
            var httpClientRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
            };
            httpClientRequestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            var temp = await this._httpClient.SendAsync(httpClientRequestMessage);
            var json = await temp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
        public Task<T> MakePostRequestAsync<T>(string url, object data = null, bool noCache = true)
        {
            var query = Enumerable.Range(0, Convert.ToInt32(noCache))
                .Select(_ => KeyValuePair.Create("t", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()))
                   .Concat(
               data?.GetType().GetProperties().Select(x => KeyValuePair.Create(x.Name, x.GetValue(data)?.ToString())) ?? Array.Empty<KeyValuePair<string, string>>())
                   .ToArray();
            if (query.Length == 0) return RequestImplAsync<T>(url);
            var temp = string.Join("&", query.Select(x => $"{x.Key}={x.Value}"));
            return RequestImplAsync<T>(url.Contains("?") ? $"&{temp}" : $"?{temp}");

        }
    }
}
