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



        /// <summary>
        /// ��һϵ��ֵ��ѡȡ��һ����Ϊ�յ��ַ���
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public string Nvl(params string[] lists)
        {
            return lists.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}
