using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public partial class EverydayRecommend
    {
        private IReadOnlyList<DailySongsItem> _dailySongs;
        private IReadOnlyList<DailySongsItem> _displayDailySongs;

        private string _searchKeyWord;
        [Inject]
        private LikedProgressService LikedProgressService { get; set; }
        private void OnSearchBoxKeyDownAsync(KeyboardEventArgs eventArgs)
        {

            if (string.Equals(eventArgs.Key, "Enter", StringComparison.OrdinalIgnoreCase))
            {
                if (this._searchKeyWord?.Length > 0)
                    this._displayDailySongs = this._dailySongs.Where(x => x.name.Contains(this._searchKeyWord, StringComparison.OrdinalIgnoreCase)).ToArray();
                else if (this._displayDailySongs.Count != this._dailySongs.Count)
                    this._displayDailySongs = this._dailySongs;
            }

        }
        private Task PlayAllAsync()
        {
            return this.PlayControlFlowService.AddRange2PlaySequenceAsync(this._dailySongs.Select(StandardAdapter), clearCollection: true);
        }
        private Task PlayAsync(DailySongsItem item)
        {
            return this.PlayControlFlowService.Add2PlaySequenceAsync(StandardAdapter(item), clearCollection: false);

        }
        private Task PlaySelectedAsync()
        {
            throw new NotImplementedException();
            //var rows = this._dtPlaylist.GetCheckedItems();
            //if (rows.Count == 0) return ToastMessageService.ErrorAsync("请先选择项").AsTask();
            //return this.PlayControlFlowService.AddRange2PlaySequenceAsync(rows.Select(StandardAdapter), clearCollection: true);
        }
        private SimplePlayableItem StandardAdapter(DailySongsItem x)
        {
            return new SimplePlayableItem
            {

                Id = x.id,
                Title = x.name,
                MvId = x.mv,
                Liked = x.liked,
                Album = new Album
                {
                    id = x.al.id,
                    name = x.al.name,
                    picUrl = x.al.picUrl,

                },
                Artists = x.ar.Select(y => new Artist
                {
                    id = y.id,
                    name = y.name,


                }).ToArray()
            };
        }
        private async Task LikeOrNotAsync(DailySongsItem item)
        {
            item.liked = await this.LikedProgressService.LikedOrNotAsync(item.id, !item.liked, CanLikedMediaType.Music) ? !item.liked : item.liked;
        }
        protected override async Task OnInitializedAsync()
        {

            var temp = await this.HttpRequestService.MakePostRequestAsync<EverydayRecommendApiResultModel>("/recommend/songs");
            if (temp.data?.dailySongs?.Count > 0)
            {
                var likedIds = await this.LikedProgressService.EnsureLikedMusicIdsAsync();
                foreach (var item in temp.data.dailySongs)
                {
                    item.liked = likedIds.Contains(item.id);
                }
                this._dailySongs = temp.data.dailySongs;
                this._displayDailySongs = this._dailySongs;
            }
            else
            {
                this._dailySongs = Array.Empty<DailySongsItem>();
                this._displayDailySongs = this._dailySongs;
            }
            await base.OnInitializedAsync();
        }
    }
}
