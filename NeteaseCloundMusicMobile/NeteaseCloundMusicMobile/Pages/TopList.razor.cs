using NeteaseCloundMusicMobile.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class TopList
    {

        private IReadOnlyList<ListItem> _topPlaylists;
        private ArtistToplist _artistToplist;
        protected override async Task OnInitializedAsync()
        {
            var (toplistResult, toplistdetailResult) = await TaskWhenAllHelper.WhenAllAsync(
                HttpRequestService.MakePostRequestAsync<TopListApiResultModel>("/toplist"),
                HttpRequestService.MakePostRequestAsync<TopListDetailApiResultModel>("/toplist/detail")
                );
            this._topPlaylists = toplistResult.list.Join(toplistdetailResult.list, x => x.id, y => y.id, (x, y) =>
              {
                  x.tracks = y.tracks;
                  return x;

              }).ToArray();
            _artistToplist = toplistdetailResult.artistToplist;
            await base.OnInitializedAsync();
        }
    }
}
