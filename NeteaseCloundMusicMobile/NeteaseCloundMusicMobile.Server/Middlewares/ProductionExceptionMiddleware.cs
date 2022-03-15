using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace NeteaseCloundMusicMobile.Server.Middlewares
{
    public class ProductionExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProductionExceptionMiddleware> _logger;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                e = FlatterException(e);
                this._logger.LogError(e,$"请求发生了错误,请求地址：【{httpContext.Request.GetDisplayUrl()}】,请求方法：【{httpContext.Request.Method}】");
            }
        }

        private static Exception FlatterException(Exception e)
        {
            var result = e;
            while (result is AggregateException aggregateException)
            {
                result = aggregateException.InnerException;
            }

            return result;
        }
    }
}
