using BulmaRazor.Components;
using NeteaseCloundMusicMobile.Client.Models;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NeteaseCloundMusicMobile.Client.Services
{
    /// <summary>
    /// 用来表示音乐播放的控制
    /// </summary>
    public class PlayControlFlowService : IDisposable, IPlayControlFlowService
    {

        private enum PlayStatus
        {
            Pending,
            AccessDenied,
            Success
        }

        #region 播放模式
        private class RepeateOnePlayMode : IPlayMode
        {

            public string Name => "单曲重复";

            public string IconClass => "iconfont icon-single-loop";

            public int? OnNext(int index, int count)
            {
                return index;
            }

            public int? OnPrev(int index, int count)
            {
                return OnNext(index, count);
            }
        }

        private class RepeateAllPlayMode : IPlayMode
        {
            int IPlayMode.Sort => -1;
            public string Name => "列表循环";
            bool IPlayMode.SupportJumpTheQueue => true;
            public string IconClass => "iconfont icon-repeat";

            public int? OnNext(int index, int count)
            {
                var result = index + 1;
                if (result >= count) result = 0;

                return result;
            }

            public int? OnPrev(int index, int count)
            {
                var result = index - 1;
                if (result < 0) result = count - 1;
                return result;
            }
        }


        private class RandomPlayMode : IPlayMode
        {
            /// <summary>
            /// 提前缓存好随机顺序,以确保点击下一曲后再点击上一个回到原来的项
            /// </summary>
            private int[] _indexs;
            public string Name => "随机播放";

            public string IconClass => "iconfont icon-random";

            private void EnsureRandom(int count)
            {
                if (this._indexs?.Length == count) return;
                _indexs = Enumerable.Range(0, count).OrderBy(x => System.IO.Path.GetTempFileName()).ToArray();
            }


            public int? OnNext(int index, int count)
            {
                this.EnsureRandom(count);
                var result = Array.IndexOf(this._indexs, index) + 1;
                if (result >= count) result = 0;
                return this._indexs[result];
            }

            public int? OnPrev(int index, int count)
            {
                this.EnsureRandom(count);
                var result = Array.IndexOf(this._indexs, index) - 1;
                if (result < 0) result = count - 1;
                return this._indexs[result];
            }
        }
        #endregion


        private static readonly IPlayMode[] s_support = new IPlayMode[] {

        new RandomPlayMode(),

            new RepeateOnePlayMode(),
        new RepeateAllPlayMode()
        }.OrderBy(x => x.Sort).ToArray();

        //private IPlayMode _playMode = s_support[0];


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



        public bool Loading => p_Loading;
        /// <summary>
        /// 表示支持的播放循环模式
        /// </summary>
        public virtual IReadOnlyList<IPlayMode> SupportPlayModes => s_support;


        public virtual bool SupportShowTracks => true;
        public virtual bool SupportPrev => true;
        public virtual bool SupportPlayTrack => true;
        /// <summary>
        /// 表示当前播放项
        /// </summary>
        public PlayableItemBase CurrentPlayableItem => this._audioPlayService.CurrentPlayableItem;
        public AudioPlayService AudioPlayService => this._audioPlayService;
        /// <summary>
        /// 用来表示当前的播放列表
        /// </summary>
        public virtual List<PlayableItemBase> Tracks => p_TracksCollection;

        /// <summary>
        /// 表示当前的循环模式
        /// </summary>
        public IPlayMode PlayMode
        {
            get;
            private set;

        } = s_support[0];

        private async ValueTask<PlayStatus> SafePlayAsync(OneOf<int, PlayableItemBase> indexOrItem)
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
        public PlayControlFlowService(AudioPlayService audioPlayService,
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

        /// <summary>
        /// 添加到播放列表
        /// </summary>
        /// <returns></returns>
        public virtual async Task Add2PlaySequenceAsync(PlayableItemBase playableItem, bool autoPlay = true, bool clearCollection = false)
        {
            if (clearCollection) this.p_TracksCollection.Clear();
            var existsItem = this.p_TracksCollection.Find(x => x.Equals(playableItem));
            if (existsItem == null)
            {
                this.p_TracksCollection.Add(playableItem);
                existsItem = playableItem;
            }
            if (autoPlay)
            {
                await SafePlayAsync(existsItem);
            }
        }
        public virtual async Task AddRange2PlaySequenceAsync(IEnumerable<PlayableItemBase> playableItems, bool autoPlay = true, bool clearCollection = false)
        {
            if (clearCollection) this.p_TracksCollection.Clear();
            this.p_TracksCollection.AddRange(playableItems);
            if (autoPlay && this.p_TracksCollection.Count > 0)
            {
                await SafePlayAsync(this.p_TracksCollection[0]);

            }
        }


        public virtual async Task<bool> JumpTheQueueAsync(PlayableItemBase playableItem)
        {
            if (!this.PlayMode.SupportJumpTheQueue) return false;


            if (this.CurrentPlayableItem == null) return await Add2PlaySequenceAsync(playableItem).ContinueWith(x => x.IsCompletedSuccessfully);
            if (CurrentPlayableItem.Equals(playableItem)) return false;

            var existIndex = this.p_TracksCollection.FindIndex(x => x.Equals(playableItem));
            if (existIndex >= 0) this.p_TracksCollection.RemoveAt(existIndex);
            var index = this.p_TracksCollection.IndexOf(CurrentPlayableItem);
            this.p_TracksCollection.Insert(index + 1, playableItem);
            return true;
        }
        /// <summary>
        /// 下一个
        /// </summary>
        public void ChangePlayModel()
        {
             
            var index = IndexOf(SupportPlayModes, this.PlayMode) + 1;
            if (index >= SupportPlayModes.Count) index = 0;
            this.PlayMode = SupportPlayModes[index];
        }


        public virtual async Task<bool> NextAsync()
        {
            var temp = await NextImplAsync(null).ConfigureAwait(false);
            if (temp && SuccessfulNextExecute != null) SuccessfulNextExecute.Invoke(this, EventArgs.Empty);
            return temp;
        }
        public virtual Task<bool> PrevAsync() => PrevImplAsync(null);
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


        private static int IndexOf<T>(IReadOnlyList<T> lst, T item)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if(lst[i].Equals(item)) return i;
            }

            return -1;
        }
        private async Task<bool> PrevImplAsync(int? currentIndex)
        {
            if (this.p_TracksCollection.Count == 0) return false;
            currentIndex ??= this.p_TracksCollection.IndexOf(CurrentPlayableItem);
            var index = this.PlayMode.OnPrev(currentIndex.Value, this.p_TracksCollection.Count);
            if (!index.HasValue) return false;
            var temp = await this.SafePlayAsync(index.Value).ConfigureAwait(false);
            if (temp == PlayStatus.AccessDenied)
            {
                return await PrevImplAsync(index);
            }
            return temp == PlayStatus.Success;
        }



        public void Dispose()
        {
            _positionChangedSubscriber?.Dispose();
        }
    }


}
