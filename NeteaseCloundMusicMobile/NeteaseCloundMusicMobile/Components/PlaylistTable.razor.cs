using BulmaRazor.Components;
using Microsoft.AspNetCore.Components;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Components
{
    public partial class PlaylistTable
    {

        private IReadOnlyList<SongsItem> _displayTracks = Array.Empty<SongsItem>();
        private DataTable<SongsItem> _dtPlaylist;
        private string _searchKeyWord;
        [Inject]
        private LikedProgressService LikedProgressService { get; set; }


        [Parameter]
        public IReadOnlyList<SongsItem> Tracks { get; set; }
        [Parameter]

        public bool EnableSearch { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (Tracks == null) throw new NotSupportedException();
            var likedIds = await this.LikedProgressService.EnsureLikedMusicIdsAsync();
            foreach (var item in Tracks)
            {
                item.liked = likedIds.Contains(item.id);
            }

            this._displayTracks = Tracks;
        }
        public Task PlayAllAsync()
        {
            return this.PlayControlFlowService.AddRange2PlaySequenceAsync(Tracks.Select(StandardAdapter), clearCollection: true);
        }
        public Task PlayAsync(SongsItem item)
        {
            return this.PlayControlFlowService.Add2PlaySequenceAsync(StandardAdapter(item), clearCollection: false);

        }

        public Task PlaySelectedAsync()
        {
            var rows = this._dtPlaylist.GetCheckedItems();
            if (rows.Count == 0) return ToastMessageService.ErrorAsync("请先选择项").AsTask();
            return this.PlayControlFlowService.AddRange2PlaySequenceAsync(rows.Select(StandardAdapter), clearCollection: true);
        }
        private async Task LikeOrNotAsync(SongsItem item)
        {
            item.liked = await this.LikedProgressService.LikedOrNotAsync(item.id, !item.liked, CanLikedMediaType.Music) ? !item.liked : item.liked;
        }
        private void OnSearchBoxKeyDownAsync(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs eventArgs)
        {

            if (string.Equals(eventArgs.Key, "Enter", StringComparison.OrdinalIgnoreCase))
            {
                if (this._searchKeyWord?.Length > 0)
                    this._displayTracks = this.Tracks.Where(x => x.name.Contains(this._searchKeyWord, StringComparison.OrdinalIgnoreCase)).ToArray();
                else if (this._displayTracks.Count != this.Tracks.Count)
                    this._displayTracks = this.Tracks;
            }

        }
        private SimplePlayableItem StandardAdapter(SongsItem x)
        {
            return new SimplePlayableItem
            {

                Id = x.id,
                Title = x.name,
                MvId = x.mv,
                Duration=x.dt,
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
    }
}
