using Microsoft.JSInterop;

using NeteaseCloundMusicMobile.Client.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Services
{
    /// <summary>
    /// 此服务封装播放相关的接口
    /// </summary>
    public class AudioPlayService : IDisposable
    {
        private readonly IJSInProcessRuntime _jSRuntime;

        private IDisposable _positionSubscribe;

        public event EventHandler<string> AudioStateChanged;
        /// <summary>
        /// 当前播放的项
        /// </summary>
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


        /// <summary>
        /// 是否静音
        /// </summary>
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
        public AudioPlayService(IJSRuntime jSRuntime)
        {
            this._jSRuntime = jSRuntime as IJSInProcessRuntime;

        }
        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public async Task PlayAsync(PlayableItemBase item)
        {
            if (item != null)
            {
                this.CurrentPlayableItem = item;


            }
            if (this.CurrentPlayableItem == null) return;
            await this._jSRuntime.InvokeVoidAsync("audioPlayer.play", item?.Url ?? string.Empty);
            InvokeEvent();


            if (this._positionSubscribe == null)
                this._positionSubscribe = Observable.Interval(TimeSpan.FromSeconds(0.5),ThreadPoolScheduler.Instance)
                     .Subscribe(InvokePositionChanged);


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
        /// <summary>
        /// 触发对应的事件
        /// </summary>
        /// <param name="names"></param>
        private void InvokeEvent([CallerMemberName] string names = null)
        {
            if (AudioStateChanged!=null)
            {
                
                AudioStateChanged.Invoke(this, names);
            }
           
        }
        /// <summary>
        /// 对应
        /// </summary>
        /// <param name="args"></param>

        private void InvokePositionChanged(long args)
        {

            this.InvokeEvent(nameof(Position));

        }

        public void Dispose()
        {
            _positionSubscribe?.Dispose();
        }
    }
}
