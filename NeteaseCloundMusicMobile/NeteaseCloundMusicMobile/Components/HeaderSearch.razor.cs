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
                if (item is SuggestSongsItem song)
                {
                    await this.PlayControlFlowService.Add2PlaySequenceAsync(StandardAdapter(song), clearCollection: false);

                }
            }
            finally
            {
                this._searchSuggestModel = null;
            }
        }
        private SimplePlayableItem StandardAdapter(SuggestSongsItem x)
        {

            return new SimplePlayableItem
            {

                Id = x.id,
                Title = x.name,
                MvId = x.mv,
                Duration = x.dt,
                Liked = x.liked,

                Artists = x.artists.Select(y => new Models.Artist
                {
                    id = y.id,
                    name = y.name,


                }).ToArray()
            };
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
            Console.WriteLine(keywords);
            if (string.IsNullOrWhiteSpace(keywords))
            {
                _searchSuggestModel = null;

            }
            else
                _searchSuggestModel = await this.HttpRequestService.MakePostRequestAsync<SearchSuggestApiResultModel>("/search/suggest", new { keywords }).ContinueWith(x => x.Result.result);
            StateHasChanged();
        }
        public void Dispose()
        {
            _objRef?.Dispose();
        }
       

    }
}
