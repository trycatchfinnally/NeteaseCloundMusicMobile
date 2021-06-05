using Microsoft.JSInterop;

using NeteaseCloundMusicMobile.Client.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Services
{
    public class AudioPlayService
    {
        private readonly IJSInProcessRuntime _jSRuntime;
        private readonly IHttpRequestService _httpRequestService;
       


        public PlayableItemBase CurrentPlayableItem { get; private set; }

        /// <summary>
        /// 用来表示音量
        /// </summary>
        public double Volumn
        {
            get
            {
                return this.GetPropertyFromJavaScript<double>("volumn");
            }
            set
            {
                this.SetPropertyFromJavascript("volumn", value);
            }
        }



        public bool Muted
        {
            get
            {
                return GetPropertyFromJavaScript<bool>("muted");
            }
            set
            {
                this.SetPropertyFromJavascript("muted", value);
            }
        }

        /// <summary>
        /// 中的时长
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                return TimeSpan.FromSeconds(GetPropertyFromJavaScript<double>("duration"));
            }
        }

        /// <summary>
        /// 表示当前的播放位置
        /// </summary>
        public TimeSpan Position
        {
            get
            {
                return TimeSpan.FromSeconds(GetPropertyFromJavaScript<double>("currentTime"));
            }
            set
            {
                this.SetPropertyFromJavascript("currentTime", value.TotalSeconds);
            }
        }
        /// <summary>
        /// 表示是否暂停或者停止状态
        /// </summary>
        public bool Paused => this.GetPropertyFromJavaScript<bool>("paused");
        public AudioPlayService(IJSRuntime jSRuntime, IHttpRequestService httpRequestService)
        {
            this._jSRuntime = jSRuntime as IJSInProcessRuntime;
            this._httpRequestService = httpRequestService;
        }
       

        public async Task PlayAsync(PlayableItemBase item)
        {
            if (item != null)
            {
                this.CurrentPlayableItem = item;
                await this.CurrentPlayableItem.EnsureUrlAsync(_httpRequestService);

            }
            this._jSRuntime.InvokeVoid("audioPlayer.play", item?.Url);
        }
        /// <summary>
        /// 暂停播放
        /// </summary>
        /// <returns></returns>
        public void Pause()
        {
            this._jSRuntime.InvokeVoid("audioPlayer.pause()");

        }
        private T GetPropertyFromJavaScript<T>(string propName)
        {
            return this._jSRuntime.Invoke<T>($"audioPlayer.{propName}");
        }

        private void SetPropertyFromJavascript(string propName, object value)
        {
            this._jSRuntime.InvokeVoid($"audioPlayer.{propName}={value}");

        }
    }
}
