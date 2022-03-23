using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using BulmaRazor.Components;
using Microsoft.Extensions.Logging;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services.Features;

namespace NeteaseCloundMusicMobile.Client.Services
{
    /// <summary>
    /// 通过直接继承的方式减少去编写代码
    /// </summary>
    public class PersonalFmPlayControlFlowService : PlayControlFlowBase, 
        ISupportGarbageFeature
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

            PlayMode=  new PersonalFmSealedPlayModel();


            InitComponentAsync().ToObservable().Subscribe(_=>Console.WriteLine(234),e=>Console.WriteLine(e.Message));

        }


        public override IPlayMode PlayMode { get; }

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

        public async Task<bool> GarbageTrackAsync(PlayableItemBase item)
        {
            var temp = await this.p_HttpRequestService
                .MakePostRequestAsync<ApiResultModelRootBase>($"/fm_trash?id={item.Id}").ConfigureAwait(false);
            var result= temp.code == 200;
            if(result) { await NextAsync().ConfigureAwait(false); }
            return result;

        }
    }
}
