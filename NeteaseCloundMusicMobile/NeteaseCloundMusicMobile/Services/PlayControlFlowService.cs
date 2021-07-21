using BulmaRazor.Components;
using NeteaseCloundMusicMobile.Client.Models;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Services
{
    /// <summary>
    /// 用来表示音乐播放的控制
    /// </summary>
    public class PlayControlFlowService : IDisposable
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

        private IPlayMode _playMode = s_support[0];
        private bool m_Loading = false;//用来标识是否加载中
        private readonly AudioPlayService _audioPlayService;
        private readonly IHttpRequestService _httpRequestService;
        private readonly ToastService _toastService;
        private readonly List<PlayableItemBase> _tracksCollection = new List<PlayableItemBase>();


        /// <summary>
        /// 表示支持的播放循环模式
        /// </summary>
        public IReadOnlyList<IPlayMode> SupportPlayModes => s_support;
        /// <summary>
        /// 表示当前播放项
        /// </summary>
        public PlayableItemBase CurrentPlayableItem => this._audioPlayService.CurrentPlayableItem;
        public AudioPlayService AudioPlayService => this._audioPlayService;
        /// <summary>
        /// 用来表示当前的播放列表
        /// </summary>
        public List<PlayableItemBase> Tracks => _tracksCollection;
        /// <summary>
        /// 表示当前的循环模式
        /// </summary>
        public IPlayMode PlayMode
        {
            get => _playMode;

        }

        private async ValueTask<PlayStatus> SafePlayAsync(OneOf<int, PlayableItemBase> indexOrItem)
        {
            if (m_Loading) return PlayStatus.Pending;
            m_Loading = true;
            try
            {
                var item = indexOrItem.IsT0 ? this._tracksCollection[indexOrItem.AsT0] : indexOrItem.AsT1;
                if (!await item.EnsureUrlAsync(this._httpRequestService))
                {
                    await this._toastService.ErrorAsync("亲爱的，暂无版权");
                    return PlayStatus.AccessDenied;
                }
                await this._audioPlayService.PlayAsync(item);
                return PlayStatus.Success;
            }
            finally
            {
                m_Loading = false;
            }
        }
        private async void AudioPlayService_AudioStateChanged(object sender, string e)
        {

            switch (e)
            {

                case nameof(AudioPlayService.Position):
                    if (Math.Abs((this._audioPlayService.Position - this._audioPlayService.Duration).TotalSeconds) <= 1)
                    {
                        if (m_Loading) return;//加载完了自己会走下一个流程
                        var bNext = await this.NextAsync();
                        var maxCount = 0;
                        while (!bNext && maxCount++ < this._tracksCollection.Count)
                        {
                            bNext = await this.NextAsync();
                        }
                    }
                    break;
            }
        }
        public PlayControlFlowService(AudioPlayService audioPlayService,
            IHttpRequestService httpRequestService,
            ToastService toastService)
        {
            _audioPlayService = audioPlayService;
            this._httpRequestService = httpRequestService;
            this._toastService = toastService;


            this._audioPlayService.AudioStateChanged += AudioPlayService_AudioStateChanged;

        }

        /// <summary>
        /// 添加到播放列表
        /// </summary>
        /// <returns></returns>
        public async Task Add2PlaySequenceAsync(PlayableItemBase playableItem, bool autoPlay = true, bool clearCollection = false)
        {
            if (clearCollection) this._tracksCollection.Clear();
            var existsItem = this._tracksCollection.Find(x => x.Equals(playableItem));
            if (existsItem == null)
            {
                this._tracksCollection.Add(playableItem);
                existsItem = playableItem;
            }
            if (autoPlay)
            {
                await SafePlayAsync(existsItem);
            }
        }
        public async Task AddRange2PlaySequenceAsync(IEnumerable<PlayableItemBase> playableItems, bool autoPlay = true, bool clearCollection = false)
        {
            if (clearCollection) this._tracksCollection.Clear();
            this._tracksCollection.AddRange(playableItems);
            if (autoPlay && this._tracksCollection.Count > 0)
            {
                await SafePlayAsync(this._tracksCollection[0]);

            }
        }


        public async Task<bool> JumpTheQueueAsync(PlayableItemBase playableItem)
        {
            if (!this.PlayMode.SupportJumpTheQueue) return false;
            if (this.CurrentPlayableItem == null)return await Add2PlaySequenceAsync(playableItem).ContinueWith(x=>x.IsCompletedSuccessfully);
            var index = this._tracksCollection.IndexOf(CurrentPlayableItem);
            this._tracksCollection.Insert(index+1,playableItem);
            return true;
        }
        /// <summary>
        /// 下一个
        /// </summary>
        public void ChangePlayModel()
        {
            var index = Array.IndexOf(s_support, this._playMode) + 1;
            if (index >= s_support.Length) index = 0;
            this._playMode = s_support[index];
        }


        public Task<bool> NextAsync() => NextImplAsync(null);
        public Task<bool> PrevAsync() => PrevImplAsync(null);
        private async Task<bool> NextImplAsync(int? currentIndex )
        {
            if (this._tracksCollection.Count == 0) return false;
            currentIndex ??= this._tracksCollection.IndexOf(CurrentPlayableItem);
            var index = this._playMode.OnNext(currentIndex.Value, this._tracksCollection.Count);
            if (!index.HasValue) return false;
            var temp = await this.SafePlayAsync(index.Value);
            if (temp == PlayStatus.AccessDenied)
            {
                return await NextImplAsync(index);
            }
            return temp == PlayStatus.Success;
        }



        private async Task<bool> PrevImplAsync(int? currentIndex )
        {
            if (this._tracksCollection.Count == 0) return false;
            currentIndex ??= this._tracksCollection.IndexOf(CurrentPlayableItem);
            var index = this._playMode.OnPrev(currentIndex.Value, this._tracksCollection.Count);
            if (!index.HasValue) return false;
            var temp = await this.SafePlayAsync(index.Value);
            if (temp == PlayStatus.AccessDenied)
            {
                return await PrevImplAsync(index);
            }
            return temp == PlayStatus.Success;
        }



        public void Dispose()
        {
            this._audioPlayService.AudioStateChanged -= AudioPlayService_AudioStateChanged;
        }
    }


    /// <summary>
    /// 表示一系列播放模式
    /// </summary>
    public interface IPlayMode
    {
        /// <summary>
        /// 该播放模式是否支持插队播放
        /// </summary>
        bool SupportJumpTheQueue => false;
        /// <summary>
        /// 表示显示顺序
        /// </summary>
        int Sort => 0;
        /// <summary>
        /// 表示播放模式的名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 表示模式的图标类名
        /// </summary>
        string IconClass { get; }
        int? OnNext(int index, int count);
        int? OnPrev(int index, int count);

    }
}
