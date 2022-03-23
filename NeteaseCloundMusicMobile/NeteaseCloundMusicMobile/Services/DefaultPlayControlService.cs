using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services.Features;

namespace NeteaseCloundMusicMobile.Client.Services
{
    public class DefaultPlayControlService : PlayControlFlowBase, 
        ISupportPlayModeFeature,
        ISupportPrevFeature,
        ISupportShowTracksFeature,
        ISupportManualAddTracksFeature
    {

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
        public DefaultPlayControlService(AudioPlayService audioPlayService,
            IHttpRequestService httpRequestService) : base(audioPlayService, httpRequestService)
        {

        }
        private static int IndexOf<T>(IReadOnlyList<T> lst, T item)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].Equals(item)) return i;
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
        public IReadOnlyList<IPlayMode> SupportPlayModes => s_support;

        /// <summary>
        /// 表示当前的循环模式
        /// </summary>
        public override IPlayMode PlayMode => _playMode;

        public List<PlayableItemBase> Tracks => p_TracksCollection;

        /// <summary>
        /// 下一个
        /// </summary>
        public void ChangePlayModel()
        {

            var index = IndexOf(SupportPlayModes, this.PlayMode) + 1;
            if (index >= SupportPlayModes.Count) index = 0;
            this._playMode = SupportPlayModes[index];
        }

        public Task<bool> PrevAsync() => PrevImplAsync(null);

        public   async Task Add2PlaySequenceAsync(PlayableItemBase playableItem, bool autoPlay = true, bool clearCollection = false)
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
        public   async Task AddRange2PlaySequenceAsync(IEnumerable<PlayableItemBase> playableItems, bool autoPlay = true, bool clearCollection = false)
        {
            if (clearCollection) this.p_TracksCollection.Clear();
            this.p_TracksCollection.AddRange(playableItems);
            if (autoPlay && this.p_TracksCollection.Count > 0)
            {
                await SafePlayAsync(this.p_TracksCollection[0]);

            }
        }


    }
}
