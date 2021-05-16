using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            ConfigureService(builder.Services);
            await builder.Build().RunAsync();
        }


        private static IServiceCollection ConfigureService(IServiceCollection services)
        {
            services.AddScoped(_ =>
            {

                var httpClient = new HttpClient { BaseAddress = new Uri("http://home.tangkh.top:63310") };
                httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Methods", "*");
                return httpClient;
            });

            services.AddScoped<IHttpRequestService, HttpRequestService>();
            services.AddBulmaRazor();
            services.AddLogging();
            services.AddBlazoredLocalStorage();
            return services;
        }
    }
}
