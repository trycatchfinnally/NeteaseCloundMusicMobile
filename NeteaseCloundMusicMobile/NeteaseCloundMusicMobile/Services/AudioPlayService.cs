using Microsoft.JSInterop;

using NeteaseCloundMusicMobile.Client.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Services
{
    public class AudioPlayService
    {
        private readonly IJSInProcessRuntime _jSRuntime;
        private readonly IHttpRequestService _httpRequestService;
        private bool _postionChangedStart = false;
        public event EventHandler<string> AudioStateChanged;

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
                this.SetPropertyFromJavascript("muted", Convert.ToInt32(value));
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
                

            }
            if (this.CurrentPlayableItem == null) return;
            await this._jSRuntime.InvokeVoidAsync("audioPlayer.play", item?.Url ?? string.Empty);
            InvokeEvent();
            if (!this._postionChangedStart)
            {
                this._postionChangedStart = true;
                _ = this.InvokePositionChangedAsync();
            }
        }
        /// <summary>
        /// 暂停播放
        /// </summary>
        /// <returns></returns>
        public void Pause()
        {
            this._jSRuntime.InvokeVoid("audioPlayer.pause");
            InvokeEvent();


        }
        private T GetPropertyFromJavaScript<T>(string propName)
        {
            return this._jSRuntime.Invoke<T>("eval", $"audioPlayer.{propName}");
        }

        private void SetPropertyFromJavascript(string propName, object value, [CallerMemberName] string names = null)
        {
            this._jSRuntime.InvokeVoid("eval", $"audioPlayer.{propName}={value}");
            InvokeEvent(names);
        }

        private void InvokeEvent([CallerMemberName] string names = null)
        {
            
            AudioStateChanged?.Invoke(this, names);
        }


        private async Task InvokePositionChangedAsync()
        {
            while (true)
            {
                await Task.Delay(500);
                if (this.Paused) continue;
                this.InvokeEvent(nameof(Position));
            }
        }
    }
}
