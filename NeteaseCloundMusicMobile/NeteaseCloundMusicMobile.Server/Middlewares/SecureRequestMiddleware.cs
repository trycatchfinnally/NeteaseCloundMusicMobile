using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Server.Middlewares
{
    public class SecureRequestMiddleware
    {


        private readonly RequestDelegate _next;

        public SecureRequestMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        private static DateTime TimeStamp2DateTime(long timeStamp)
        {
            return new DateTime(1970, 1, 1).AddSeconds(timeStamp);
        }
        private static string Md5Encrypt(string input)
        {
            input ??= string.Empty;//添加，免得报错
            var md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            string result = BitConverter.ToString(md5.ComputeHash(bytes));
            return result.Replace("-", "").ToLower();
        }

        private static Task ResponseWriteAsync(HttpContext httpContext, string msg)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            httpContext.Response.ContentType = "text/html;charset=utf-8";
            return httpContext.Response.WriteAsync(msg);
        }
      

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Path.StartsWithSegments(new PathString("/api")))
            {
                await this._next.Invoke(httpContext); return;
            }
            if (!httpContext.Request.Query.TryGetValue("sign", out var sign))
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await ResponseWriteAsync(httpContext, "很抱歉，你没有权限访问此接口，请在查询字符串中提供有效的签名");
                return;
            }

            if (!httpContext.Request.Query.TryGetValue("t", out var timeStampString))//时间戳
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await ResponseWriteAsync(httpContext, "很抱歉，你没有权限访问此接口，请在查询字符串中提供请求的时间戳");
                return;
            }

            if (!long.TryParse(timeStampString, out var timeStamp))
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await ResponseWriteAsync(httpContext, "很抱歉，你没有权限访问此接口，查询字符串中请求的时间戳格式错误");
                return;
            }

            if ((DateTime.UtcNow - TimeStamp2DateTime(timeStamp)).TotalMinutes > 5)//超时时间5分钟
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await ResponseWriteAsync(httpContext, "请求已超时，请重新发起请求");
                return;
            }
            var tmpSign = String.Empty;

            if (string.Equals(httpContext.Request.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase)
                || string.Equals(httpContext.Request.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase))
            {
                var needSign = httpContext.Request.Query.Where(x => x.Key != "sign").OrderBy(x => x.Key);
                var sb =new StringBuilder() .Append("fsdagdfsg.rtyfd");
                foreach (var item in needSign)
                    sb.Append(item.Value.ToString());
                sb.Append("ghjftrhjrtyeyfdgsfg");
                tmpSign = Md5Encrypt(sb.ToString());

            }
            
            else//暂不支持其他请求类型
            {
                httpContext.Response.StatusCode = StatusCodes.Status501NotImplemented;
                return;
            }
            if (!string.Equals(tmpSign, sign, StringComparison.OrdinalIgnoreCase))
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await ResponseWriteAsync(httpContext, "很抱歉，你没有权限访问此接口，你所提供的签名错误");
                return;
            }
            await this._next.Invoke(httpContext);

        }
    }
}
