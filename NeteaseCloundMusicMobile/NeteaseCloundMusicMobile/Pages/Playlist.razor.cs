using BulmaRazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class Playlist
    {
       
        private class SubScribersQuery
        {
            public int limit { get; set; }
            public int offset { get; set; }
            public long id { get; set; }
        }
        private Models.Playlist _playlist;
        private Components.PlaylistTable _playlistTable;
        private SubScribersQuery _subScribersQuery;
        private PlaylistSubscribersApiResultModel _subscribers;
        
        private IReadOnlyList<TracksItem> _displayTracks = Array.Empty<TracksItem>();

        [Parameter]
        public long Id { get; set; }
        [Inject]
        private LikedProgressService LikedProgressService { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            var temp = await this.HttpRequestService.MakePostRequestAsync<PlaylistDetailApiResultModel>("/playlist/detail", new { id = Id });
            this._playlist = temp.playlist;
            if (this._playlist == null)
            {
                NotFound();
                return;
            }
            try
            {


                var groups = this._playlist.trackIds
                      .Where(x => this._playlist.tracks.All(y => y.id != x.id))
                      .Select((x, i) => KeyValuePair.Create(x.id, i))
                      .GroupBy((x) => x.Value / 30)//每组数量最多30
                      .ToDictionary(x => x.Key, x => this.HttpRequestService.MakePostRequestAsync<GetSongDetailResultModel>("/song/detail", new { ids = string.Join(",", x.Select(y => y.Key)) }));
                if (groups.Count == 0) return;
                this._subScribersQuery = new SubScribersQuery
                {
                    limit = 20,
                    id = Id,
                    offset = 0
                };
                var subTask = FetchSubscribersAsync();
                //为了避免id数量过多，分组调接口
                await Task.WhenAll(groups.Values.Concat<Task>(new[] { subTask }));
                var others = groups.Values.Select(x => x.Result).SelectMany(x => x.songs);
                this._playlist.tracks.AddRange(others.Select(x => new TracksItem
                {

                    id = x.id,
                    name = x.name,
                    ar = x.ar,
                    al = x.al,
                    mv = x.mv,
                    dt = x.dt

                }));

            }
            finally
            {
               
                this._displayTracks = this._playlist.tracks;
            }
        }
        private async Task FetchSubscribersAsync(int pageIndex = 1)
        {
            this._subScribersQuery.offset = pageIndex * this._subScribersQuery.limit - this._subScribersQuery.limit;
            var temp = await this.HttpRequestService.MakePostRequestAsync<PlaylistSubscribersApiResultModel>("/playlist/subscribers", this._subScribersQuery);
            this._subscribers = temp;
        }
        private Task PlayAllAsync()
        {
            return _playlistTable.PlayAllAsync();
        }

       
       
       



    }
}
