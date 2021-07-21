using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using NeteaseCloundMusicMobile.Client.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Components
{
    /// <summary>
    /// 这里独立成组件是为了避免频繁变化导致外层组件频繁刷新
    /// </summary>
    public partial class HeaderSearch
    {

        private TimeSpan _fetchFrequency = TimeSpan.FromMilliseconds(1000);
        private SearchSuggestResponseModel _searchSuggestModel;
        private ElementReference _searchElement;
        private string _keyword;
        private bool _focus;
        private DotNetObjectReference<HeaderSearch> _objRef;
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        private static readonly Dictionary<string, KeyValuePair<string, string>> m_orderNames = new()
        {
            { "songs", KeyValuePair.Create("单曲", "fa fa-music") },
            { "albums", KeyValuePair.Create("专辑", "fa fa-music") },
            { "artists", KeyValuePair.Create("歌手", "fa fa-music") },
            { "playlists", KeyValuePair.Create("歌单", "iconfont icon-gedan") },
        };
        private async Task SuggestItemClickAsync(INameIdModel item)
        {
            try
            {
                if (item is Artist)
                {
                    this.NavigationManager.NavigateTo($"/artist/{item.id}"); return;
                }
                if (item is Playlist)
                {
                    this.NavigationManager.NavigateTo($"/playlist/{item.id}"); return;
                }
                if (item is Album)
                {
                    this.NavigationManager.NavigateTo($"/album/{item.id}"); return;
                }
                if (item is SuggestSongsItem)
                {
                    var temp =
                        await this.HttpRequestService.MakePostRequestAsync<GetSongDetailResultModel>("/song/detail",
                            new { ids = item.id });
                    if (temp.songs?.Count > 0)
                        await this.PlayControlFlowService.Add2PlaySequenceAsync(StandardAdapter(temp.songs[0]), clearCollection: false);

                }
            }
            finally
            {
                this._searchSuggestModel = null;
            }
        }

        private SimplePlayableItem StandardAdapter(SongsItem x)
        {
            return new SimplePlayableItem
            {

                Id = x.id,
                Title = x.name,
                MvId = x.mv,
                Duration = x.dt,
                Liked = x.liked,
                Album = new Album
                {
                    id = x.al.id,
                    name = x.al.name,
                    picUrl = x.al.picUrl,

                },
                Artists = x.ar.Select(y => new Models.Artist
                {
                    id = y.id,
                    name = y.name,


                }).ToArray()
            };
        }

        private async Task OnLeaveInputAsync()
        {
            _focus = false;
            await Task.Delay(500);
            if (!_focus) this._searchSuggestModel = null;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _objRef = DotNetObjectReference.Create(this);
                await this.JSRuntime.InvokeVoidAsync("searchProgress.initComponent", _objRef, this._searchElement);
            }
            await base.OnAfterRenderAsync(firstRender);

        }

        [JSInvokable]
        public async Task DoSearchAsync(string keywords)
        {
            if (string.IsNullOrWhiteSpace(keywords))
                _searchSuggestModel = null;
            else
                _searchSuggestModel = await this.HttpRequestService.MakePostRequestAsync<SearchSuggestApiResultModel>("/search/suggest", new { keywords }).ContinueWith(x => x.Result.result);
            this._keyword = keywords;
            StateHasChanged();
        }
        public void Dispose()
        {
            _objRef?.Dispose();
        }


    }
}
