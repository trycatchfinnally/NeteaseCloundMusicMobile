using NeteaseCloundMusicMobile.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class MorePlaylist
    {
        private class QueryListDto
        {
            public int limit { get; set; }
            public int offset { get; set; }
            public string cat { get; set; }
            public string order { get; set; }
        }
        private string[] _constTags = new[] { "华语", "流行", "摇滚", "民谣", "电子", "另类/独立", "轻音乐", "综艺", "影视原生", "ACG" };

        private IReadOnlyList<PlaylistTag> _highqualityTags = Array.Empty<PlaylistTag>();
        private QueryListDto _query = new QueryListDto { limit = 30 };
        private TopPlaylistApiResultModel _topPlaylists;

        private IReadOnlyList<KeyValuePair<string, SubItem[]>> _playlistCatLists;
        private bool _fetching = false;
        private bool _catModalActive = false;
        protected override async Task OnInitializedAsync()
        {
            this.NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            PlaylistCatListApiResultModel temp;
            (temp,
                _, _highqualityTags) = await TaskWhenAllHelper.WhenAllAsync(this.HttpRequestService.MakePostRequestAsync<PlaylistCatListApiResultModel>("/playlist/catlist"),
                FetchListAsync(true),
                this.HttpRequestService.MakePostRequestAsync<HighqualityTagsApiResultModel>("/playlist/highquality/tags").ContinueWith(x => x.Result.tags)
                );
            this._constTags = temp.sub.OrderByDescending(x => x.resourceCount).Select(x => x.name).Take(10).ToArray();
            _playlistCatLists = temp.sub.GroupBy(x => x.category).Select(x => KeyValuePair.Create(x.Key.ToString(), x.ToArray()))
                .Join(temp.categories, x => x.Key, y => y.Key, (x, y) =>
                {

                    return KeyValuePair.Create(y.Value, x.Value);
                }).ToArray();
            await base.OnInitializedAsync();

        }

        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            _ = FetchListAsync(true).ContinueWith(x=> _catModalActive=false).ContinueWith(x=>StateHasChanged());
        }
        private async Task<bool> FetchListAsync(bool reFetch)
        {
            _fetching = true;
            if (reFetch)
            {
                this._query.offset = 0;
                this._query.cat = HttpUtility.ParseQueryString(this.NavigationManager.ToAbsoluteUri(this.NavigationManager.Uri).Query).Get("tag");

            }


            var temp = await HttpRequestService.MakePostRequestAsync<TopPlaylistApiResultModel>("/top/playlist", this._query);
            if (!reFetch)
            {

                temp.Playlists.InsertRange(0, _topPlaylists?.Playlists ?? Enumerable.Empty<Models.Playlist>());
            }
            this._topPlaylists = temp;
            _fetching = false;
            return true;
        }
        private Task FetchNextPageAsync()
        {
            this._query.offset += this._query.limit;
            return FetchListAsync(false);
        }
        public void Dispose()
        {
            this.NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
        }
    }
}
