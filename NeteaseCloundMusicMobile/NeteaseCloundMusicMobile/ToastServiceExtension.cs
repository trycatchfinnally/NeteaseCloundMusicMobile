using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulmaRazor.Components;
using Newtonsoft.Json;
using OneOf;

namespace NeteaseCloundMusicMobile.Client
{
    static class ToastServiceExtension
    {
        /// <summary>
        /// 设置默认显示2000毫秒
        /// </summary>
        private static readonly OneOf<int, TimeSpan> _defaultToastDuration = OneOf<int, TimeSpan>.FromT0(2000);

        private static ValueTask ShowToastImplAsync(ToastService toastService, string content, Color color,
            OneOf<int, TimeSpan>? duration = null)
        {
            if (toastService == null) throw new ArgumentNullException(nameof(toastService));
            if (!duration.HasValue) duration = _defaultToastDuration;
            var durationMillSeconds = Convert.ToInt32(duration.Value.IsT0 ? duration.Value.AsT0 : duration.Value.AsT1.TotalMilliseconds);
            return toastService.Show(new ToastOptions
            {
                Color = color,
                Message = content,
                Duration = durationMillSeconds
            });
        }
        /// <summary>
        /// 显示成功信息
        /// </summary>
        /// <param name="toastService"></param>
        /// <param name="content"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static ValueTask SuccessAsync(this ToastService toastService, string content, OneOf<int, TimeSpan>? duration = null)
        {
            return ShowToastImplAsync(toastService, content, Color.Primary, duration);
        }
        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="toastService"></param>
        /// <param name="content"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static ValueTask WarnAsync(this ToastService toastService, string content,
            OneOf<int, TimeSpan>? duration = null)
        {
            return ShowToastImplAsync(toastService, content, Color.Warning, duration);

        }
        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="toastService"></param>
        /// <param name="content"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static ValueTask ErrorAsync(this ToastService toastService, string content,
            OneOf<int, TimeSpan>? duration = null)
        {
            return ShowToastImplAsync(toastService, content, Color.Danger, duration);

        }

 
    }
}
