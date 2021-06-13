using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using NeteaseCloundMusicMobile.Client;
using NeteaseCloundMusicMobile.Client.Authentication;
using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

           builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            ConfigureService(builder.Services);
            await builder.Build().RunAsync();
        }


        private static IServiceCollection ConfigureService(IServiceCollection services)
        {
            //services.AddScoped(_ =>
            //{

            //    var httpClient = new HttpClient { BaseAddress = new Uri("http://home.tangkh.top:63310") };
              

            //    return httpClient;
            //});

            services.AddScoped<IHttpRequestService, HttpRequestService>();
            services.AddScoped<AudioPlayService>();
            services.AddScoped<PlayControlFlowService>();

            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            services.AddBulmaRazor();
            services.AddLogging();
            services.AddBlazoredLocalStorage();
            return services;
        }
    }
}
