using BulmaRazor.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using NeteaseCloundMusicMobile.Client.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using NeteaseCloundMusicMobile.Client.Utitys;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class Index : IDisposable
    {
        private CarouselOptions _carouselOptions = new CarouselOptions
        {

            Loop = true,
            Autoplay = true,
            Infinite = true,
            Duration = 1000,
            SlidesToShow = 1,
            PauseOnHover = true,



        };

        private bool _pageLoading = true;

        private IReadOnlyList<Banner> _displayBanners;
        private IReadOnlyList<RecommendPlaylist> _recommendPlaylist;
        private IReadOnlyList<PrivateContentItem> _privateContentItems;
        private IReadOnlyList<NewSongApiResultItem> _newSongApiResultItems;
        private IReadOnlyList<MvFirstApiResultItem> _mvFirstApiResultItems;
        private IReadOnlyList<DjProgramApiResultItem> _djProgramApiResultItems;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private  ILogger<Index> Logger { get; set; }
        private async Task FetchCarouseAsync()
        {
            var bannerModel = await HttpRequestService.MakePostRequestAsync<BannerApiResult>("/banner", new { type = 0 }, false);
            if (bannerModel?.banners?.Length > 0)
            {
                _displayBanners = bannerModel.banners;

            }
        }
        private async Task FetchEverydayRecommendPlaylistAsync()
        {
            var rootModel = await HttpRequestService.MakePostRequestAsync<EvenydayRecommendPlaylistApiResult>("/recommend/resource");
            this._recommendPlaylist = rootModel.recommend;
            StateHasChanged();
        }

        private async Task FetchPrivateContentAsync()
        {
            var resultModel = await this.HttpRequestService.MakePostRequestAsync<PrivateContentApiResultModel>("/personalized/privatecontent");
            this._privateContentItems = resultModel.result;
        }

        private async Task FetchNewSongsAsync()
        {
            var temp = await this.HttpRequestService.MakePostRequestAsync<NewSongsApiResultModel>("/top/song");
            this._newSongApiResultItems = temp.data;
        }
        private async Task FetchMvFirstAsync()
        {
            var temp = await this.HttpRequestService.MakePostRequestAsync<MvFirstApiResultModel>("/mv/first", new { limit = 4 });
            this._mvFirstApiResultItems = temp.data;
        }
        private async Task FetchFirstDjProgramAsync()
        {
            var temp = await this.HttpRequestService.MakePostRequestAsync<DjProgramApiResultModel>("/personalized/djprogram");
            this._djProgramApiResultItems = temp.result;
        }
        private void CarouseClick(Banner banner)
        {
            Console.WriteLine($"力点击了图片：{banner.imageUrl}");
        }

        private void MoreClick(int type)
        {

        }

        private async Task PersonalFmClickAsync()
        {

            this.GlobalFeatureCollectionService.SwitchPlayControlFlow(PlayControlFlowTypeCodes.PersonalFmPlayControlFlowTypeCode);
            var retryCount = 0;
            while (retryCount++ < 10)
            {
                if (!PlayControlFlowService.AudioPlayService.Paused)
                {
                    NavigationManager.NavigateTo("/playpanel"); return;
                }
                await Task.Delay(100).ConfigureAwait(false);

            }
            this.Logger.LogWarning("自动切换到播放页面失败");
        }


        private void AuthenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            using (FetchEverydayRecommendPlaylistAsync().ToObservable()

                       .Subscribe()) { }
        }
        private async Task PlayAsync(Models.NewSongApiResultItem item)
        {


            var temp = new SimplePlayableItem
            {
                Id = item.id,
                Title = item.name,
                Artists = item.artists,
                Album = item.album,
                MvId = item.mvid,
                Duration = item.duration

            };


            await this.Add2PlaySequenceAsync(temp);
        }


        protected override async Task OnInitializedAsync()
        {

            await base.OnInitializedAsync();
            var task1 = FetchCarouseAsync();
            var task2 = FetchEverydayRecommendPlaylistAsync();
            var task3 = FetchPrivateContentAsync();
            var task4 = FetchNewSongsAsync();
            var task5 = FetchMvFirstAsync();
            var task6 = FetchFirstDjProgramAsync();
            await Task.WhenAll(task1, task2, task3, task4, task5, task6);

            this.AuthenticationStateProvider.AuthenticationStateChanged += AuthenticationStateProvider_AuthenticationStateChanged;
            _pageLoading = false;

        }



        public void Dispose()
        {
            this.AuthenticationStateProvider.AuthenticationStateChanged -= AuthenticationStateProvider_AuthenticationStateChanged;
        }
    }
}
