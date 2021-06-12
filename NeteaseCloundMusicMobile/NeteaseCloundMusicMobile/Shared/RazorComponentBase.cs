using Microsoft.AspNetCore.Components;

using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulmaRazor.Components;

namespace NeteaseCloundMusicMobile.Client.Shared
{
    public abstract class RazorComponentBase : ComponentBase
    {
        [Inject]
        protected IHttpRequestService HttpRequestService { get; set; }


        [Inject]
        protected AudioPlayService AudioPlayService { get; set; }
        [Inject]
        protected ToastService ToastMessageService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// 在一系列值中选取第一个不为空的字符串
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        protected string Nvl(params string[] lists)
        {
            return lists.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
        }
        protected void NotFound()
        {
            this.NavigationManager.NavigateTo("/404");
        }
    }
}
