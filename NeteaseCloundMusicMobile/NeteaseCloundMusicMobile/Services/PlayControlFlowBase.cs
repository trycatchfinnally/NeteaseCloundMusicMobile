using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services.Features;
using OneOf;

namespace NeteaseCloundMusicMobile.Client.Services
{
    /// <summary>
    /// 用来用一套相对固定的做法实现默认的行为
    /// </summary>
    public abstract class PlayControlFlowBase:IDisposable, IBasicPlayControlFlowService

    {
        protected enum PlayStatus
        {
            Pending,
            AccessDenied,
            Success
        }
        private readonly IDisposable _positionChangedSubscriber;
        private readonly AudioPlayService _audioPlayService;


        /// <summary>
        /// 设置为protected是为了方便在私人fm模型中直接复用同时不暴露公共属性
        /// </summary>
        protected bool p_Loading = false;//
        /// <summary>
        /// 设置protected是为了方便在私人fm模型中直接复用
        /// </summary>

        protected readonly IHttpRequestService p_HttpRequestService;
        /// <summary>
        /// 设置为protected是为了方便在私人fm模型中直接复用同时不暴露公共属性
        /// </summary>

        protected readonly List<PlayableItemBase> p_TracksCollection = new List<PlayableItemBase>();
        /// <summary>
        /// 当成功切换到下一首的时候执行
        /// </summary>
        public event EventHandler SuccessfulNextExecute;
        public  abstract IPlayMode PlayMode { get; }
        public PlayableItemBase CurrentPlayableItem => this._audioPlayService.CurrentPlayableItem;
        public AudioPlayService AudioPlayService => this._audioPlayService;
        protected async ValueTask<PlayStatus> SafePlayAsync(OneOf<int, PlayableItemBase> indexOrItem)
        {
            if (p_Loading) return PlayStatus.Pending;
            p_Loading = true;
            try
            {
                var item = indexOrItem.IsT0 ? this.p_TracksCollection[indexOrItem.AsT0] : indexOrItem.AsT1;
                if (!await item.EnsureUrlAsync(this.p_HttpRequestService))
                {

                    return PlayStatus.AccessDenied;
                }
                await this._audioPlayService.PlayAsync(item);
                return PlayStatus.Success;
            }
            finally
            {
                p_Loading = false;
            }
        }

        private async Task<bool> NextImplAsync(int? currentIndex)
        {

            if (this.p_TracksCollection.Count == 0) return false;
            currentIndex ??= this.p_TracksCollection.IndexOf(CurrentPlayableItem);


            var index = this.PlayMode.OnNext(currentIndex.Value, this.p_TracksCollection.Count);

            if (!index.HasValue) return false;
            var temp = await this.SafePlayAsync(index.Value).ConfigureAwait(false);
            if (temp == PlayStatus.AccessDenied)
            {
                return await NextImplAsync(index);
            }
            return temp == PlayStatus.Success;
        }


        private async Task PositionEndedNextAsync()
        {
            if (p_Loading) return;//加载完了自己会走下一个流程
            var bNext = await this.NextAsync();
            var maxCount = 0;
            while (!bNext && maxCount++ < this.p_TracksCollection.Count)
            {
                bNext = await this.NextAsync();

            }
        }
        protected PlayControlFlowBase(AudioPlayService audioPlayService,
            IHttpRequestService httpRequestService
           )
        {
            _audioPlayService = audioPlayService;
            this.p_HttpRequestService = httpRequestService;


            this._positionChangedSubscriber =
                Observable
                    .FromEventPattern<string>(x => audioPlayService.AudioStateChanged += x,
                        x => audioPlayService.AudioStateChanged -= x)
                    .Where(x => x.EventArgs == nameof(AudioPlayService.Position))
                    .Select(x => x.Sender as AudioPlayService)
                    .Where(x => Math.Abs((x.Position - x.Duration).TotalSeconds) <= 1)
                    .Subscribe(x =>
                    {
                        using (PositionEndedNextAsync().ToObservable().Subscribe()) { }
                    });


        }

        public virtual async Task<bool> NextAsync()
        {
            var temp = await NextImplAsync(null).ConfigureAwait(false);
            if (temp && SuccessfulNextExecute != null) SuccessfulNextExecute.Invoke(this, EventArgs.Empty);
            return temp;
        }

        public void Dispose()
        {
            _positionChangedSubscriber?.Dispose();
            this.DisposeImpl();
        }

        /// <summary>
        /// 子类实现此方法用以处理自己的可释放资源
        /// </summary>
        protected virtual void DisposeImpl(){}
    }
}
