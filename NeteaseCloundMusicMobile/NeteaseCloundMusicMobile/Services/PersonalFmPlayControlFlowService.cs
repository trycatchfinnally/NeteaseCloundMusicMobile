using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using BulmaRazor.Components;
using Microsoft.Extensions.Logging;
using NeteaseCloundMusicMobile.Client.Models;

namespace NeteaseCloundMusicMobile.Client.Services
{
    /// <summary>
    /// 通过直接继承的方式减少去编写代码
    /// </summary>
    public class PersonalFmPlayControlFlowService : PlayControlFlowService
    {


        private sealed class PersonalFmSealedPlayModel : IPlayMode
        {

            public string Name => "顺序播放";
            public string IconClass => "iconfont icon-repeat";
            public int? OnNext(int index, int count)
            {
                var result = index + 1;
                if (result >= count) result = index;
                return result;
            }

            public int? OnPrev(int index, int count)
            {
                throw new System.NotSupportedException("当前模式不支持上一个");
            }
        }


        private readonly int M_Max_CachedTracksCount = 10;
        public PersonalFmPlayControlFlowService(AudioPlayService audioPlayService,
            IHttpRequestService httpRequestService
           ) :
            base(audioPlayService, httpRequestService)
        {

            this.SupportPlayModes = new[] { new PersonalFmSealedPlayModel() };
            this.ChangePlayModel();
            InitComponentAsync().ToObservable().Subscribe(_=>Console.WriteLine(234),e=>Console.WriteLine(e.Message));

        }

        public override IReadOnlyList<IPlayMode> SupportPlayModes { get; }
        public override bool SupportPrev => false;
        public override bool SupportShowTracks => false;
        public override bool SupportPlayTrack => false;

        /// <summary>
        /// 通过异常的形式强制提示不绑定到该项
        /// </summary>
        public override List<PlayableItemBase> Tracks => throw new NotSupportedException("当前播放模式不支持显示");

        public override Task<bool> PrevAsync()
        {
            throw new NotSupportedException("当前播放模式不支持上一个");
        }

        public override Task Add2PlaySequenceAsync(PlayableItemBase playableItem, bool autoPlay = true, bool clearCollection = false)
        {
            throw new NotSupportedException("当前播放模式添加到播放列表");
        }

        public override Task AddRange2PlaySequenceAsync(IEnumerable<PlayableItemBase> playableItems, bool autoPlay = true, bool clearCollection = false)
        {
            throw new NotSupportedException("当前播放模式添加到播放列表");
        }

        public override Task<bool> JumpTheQueueAsync(PlayableItemBase playableItem)
        {
            throw new NotSupportedException("当前播放模式不支持插队播放");
        }

        public override async Task<bool> NextAsync()
        {
            var result = await base.NextAsync();
           
            if (result)
            {
                var index = p_TracksCollection.IndexOf(CurrentPlayableItem);
                if (index >= p_TracksCollection.Count - 2)
                {
                    await EnsureNextBatchPersonalFmAsync();
                }
                if (p_TracksCollection.Count >= M_Max_CachedTracksCount)
                {
                    p_TracksCollection.RemoveRange(0, index);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取下一批次的私人fm
        /// </summary>
        /// <returns></returns>
        private async Task EnsureNextBatchPersonalFmAsync()
        {
            var temp = await p_HttpRequestService.MakePostRequestAsync<PersonalFmApiResultModel>("/personal_fm");
            p_TracksCollection.AddRange(temp.data.Select(item => new SimplePlayableItem
            {
                Id = item.id,
                Title = item.name,
                Artists = item.artists,
                Album = item.album,
                MvId = item.mvid,
                Duration = item.duration

            }));
        }


        private async Task InitComponentAsync()
        {
            p_Loading = true;
            await EnsureNextBatchPersonalFmAsync().ConfigureAwait(false);
            p_Loading = false;
            await NextAsync().ConfigureAwait(false);
          
           
        }

    }
}
