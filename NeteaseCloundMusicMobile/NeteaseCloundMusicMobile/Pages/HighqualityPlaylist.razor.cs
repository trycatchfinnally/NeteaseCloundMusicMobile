using NeteaseCloundMusicMobile.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class HighqualityPlaylist
    {
        private class QueryListDto
        {
            public int limit { get; set; }
            public long? before { get; set; }
            public string cat { get; set; }

        }
        private bool _fetching = false;
        private QueryListDto _query = new QueryListDto { limit = 30 };
        private IReadOnlyList<PlaylistTag> _highqualityTags = Array.Empty<PlaylistTag>();
        private bool _catModalActive = false;
        private HighqualityPlaylistApiResultModel _highqualityPlaylistApiResultModel;
        protected override async Task OnInitializedAsync()
        {
            this.NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            (_highqualityTags, _) = await TaskWhenAllHelper.WhenAllAsync(
                  this.HttpRequestService.MakePostRequestAsync<HighqualityTagsApiResultModel>("/playlist/highquality/tags").ContinueWith(x => x.Result.tags)
                  , FetchListAsync(true));
            await base.OnInitializedAsync();
        }
        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            _ = FetchListAsync(true).ContinueWith(x => _catModalActive = false).ContinueWith(x => StateHasChanged());
        }
        private async Task<bool> FetchListAsync(bool reFetch)
        {
            _fetching = true;
            if (reFetch)
            {
                this._query.before = null;
                this._query.cat = HttpUtility.ParseQueryString(this.NavigationManager.ToAbsoluteUri(this.NavigationManager.Uri).Query).Get("tag");

            }
            var temp = await HttpRequestService.MakePostRequestAsync<HighqualityPlaylistApiResultModel>("/top/playlist/highquality", this._query);
            if (!reFetch)
            {

                temp.playlists.InsertRange(0, _highqualityPlaylistApiResultModel?.playlists ?? Enumerable.Empty<Models.Playlist>());
            }
            _highqualityPlaylistApiResultModel = temp;
            _query.before = temp.lasttime;
            _fetching = false;
            return true;
        }
        private Task FetchNextPageAsync()
        {
            return FetchListAsync(false);
        }
        public void Dispose()
        {
            this.NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
        }
    }
}
