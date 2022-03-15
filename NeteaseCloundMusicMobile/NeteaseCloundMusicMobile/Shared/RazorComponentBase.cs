using Microsoft.AspNetCore.Components;

using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulmaRazor.Components;

namespace NeteaseCloundMusicMobile.Client.Shared
{
    /// <summary>
    /// ��������Ļ��࣬�Ա�ע��һЩͨ�õķ���
    /// </summary>
    public abstract class RazorComponentBase : ComponentBase
    {
        [Inject]
        protected IHttpRequestService HttpRequestService { get; set; }


        [Inject]
        protected PlayControlFlowService PlayControlFlowService { get; set; }
        [Inject]
        protected ToastService ToastMessageService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// ��һϵ��ֵ��ѡȡ��һ����Ϊ�յ��ַ���
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
        /// <summary>
        /// ����ָ�����͵�ת����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        protected static T ToDto<T>(object source)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(source);
            return System.Text.Json.JsonSerializer.Deserialize<T>(json);
        }
    }
}
