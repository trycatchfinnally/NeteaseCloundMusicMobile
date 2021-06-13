using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeteaseCloundMusicMobile.Server.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseBlazorFrameworkFiles();

            app.UseStaticFiles();
            app.Map("/api", subApp =>
            {
                //subApp.UseMiddleware<SecureRequestMiddleware>();
               
                subApp.UseMiddleware<ProxyHttpRequestMiddleware>();
            });
            app.UseRouting().UseEndpoints(endpoints =>
            {
               // endpoints.MapRazorPages();
                //endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
