using Consul;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace NeteaseCloundMusicMobile.Server.Middlewares
{
    public class ProxyHttpRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public ProxyHttpRequestMiddleware(RequestDelegate next,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _next = next;
            this._configuration = configuration;
            this._httpClientFactory = httpClientFactory;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var ip = this._configuration.GetSection("Consul:Ip").Value;
            var port = this._configuration.GetSection("Consul:Port").Get<int>();
            var serviceName = this._configuration.GetSection("Consul:NodeApiServiceName").Value;
            using (var client = new ConsulClient(x => x.Address = new Uri($"http://{ip}:{port}")))
            {
                var services = await client.Catalog.Service(serviceName).ContinueWith(x => x.Result.Response);
                if (services?.Length > 0)
                {
                    var service = services.ElementAt(new Random().Next(services.Length));
                    using (var httpClient = this._httpClientFactory.CreateClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("Cookie", string.Join(';', httpContext.Request.Cookies.Select(x => $"{x.Key}={x.Value}")));
                        if (string.Equals(httpContext.Request.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(httpContext.Request.Method, HttpMethods.Post, StringComparison.OrdinalIgnoreCase)
                            )
                        {
                            var streamContent = new StreamContent(httpContext.Request.Body);
                            var path = string.Concat("http://", 
                                service.ServiceAddress, ":", 
                                service.ServicePort, 
                                httpContext.Request.Path.ToString(), 
                                httpContext.Request.QueryString.Add("realIP", EnsureIpAddress(httpContext))                                
                                );
                            var response = await httpClient.PostAsync(path, streamContent);
                            var buffer = await response.Content.ReadAsByteArrayAsync();
                            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                            httpContext.Response.ContentType ="application/json";
                            foreach (var item in response.Headers)
                            {
                                httpContext.Response.Headers.Add(item.Key, new StringValues(item.Value.ToArray()));
                                //  httpContext.Response.Headers.Add(item.Key, new Microsoft.Extensions.Primitives.StringValues(string.Join(";", item.Value)));
                            }

                            await httpContext.Response.BodyWriter.WriteAsync(buffer);
                            return;
                        }
                    }
                }

            }
            await this._next.Invoke(httpContext);
        }



        private static string EnsureIpAddress(HttpContext httpContext)
        {
           
            if (httpContext.Request.Headers.ContainsKey("X-Real-IP")) return httpContext.Request.Headers["X-Real-IP"];
            if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For")) return httpContext.Request.Headers["X-Forwarded-For"];
            return httpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
