using BulmaRazor.Components;
using Microsoft.AspNetCore.Components;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using NeteaseCloundMusicMobile.Client.Utitys;

namespace NeteaseCloundMusicMobile.Client.Components
{
    public partial class PlaylistTable
    {

        private IReadOnlyList<SongsItem> _displayTracks = Array.Empty<SongsItem>();
        private DataTable<SongsItem> _dtPlaylist;
        private DotNetObjectReference<PlaylistTable> _objRef;
        private ElementReference _searchElement;
        //定义一个随机名，用以创建随机的js对象名
        private readonly string r_RandomJsObjectName = Path.GetRandomFileName().Substring(0, 8);
        [Inject]
        private LikedProgressService LikedProgressService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && EnableSearch )
            {
                _objRef = DotNetObjectReference.Create(this);
                await this.JSRuntime.InvokeVoidAsync("eval", $"window.{r_RandomJsObjectName}=new searchProgress()");
                await this.JSRuntime.InvokeVoidAsync($"{r_RandomJsObjectName}.initComponent", _objRef, this._searchElement);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public Task PlayAllAsync()
        {

            return this.AddRange2PlaySequenceAsync(Tracks.Select(StandardAdapter), clearCollection: true);
        }
        public Task PlayAsync(SongsItem item)
        {

            return this.Add2PlaySequenceAsync(StandardAdapter(item), clearCollection: false);

        }

        private Task SkipTryAsync()
        {

            var temp = this.Tracks.Where(x => x.privilege == null || x.privilege.fl * x.privilege.pl > 0)
                .Select(StandardAdapter);
            return this.AddRange2PlaySequenceAsync(temp, clearCollection: true);
        }
        public Task PlaySelectedAsync()
        {
            var rows = this._dtPlaylist.GetCheckedItems();
            if (rows.Count == 0) return ToastMessageService.ErrorAsync("请先选择项").AsTask();

            return this.AddRange2PlaySequenceAsync(rows.Select(StandardAdapter), clearCollection: true);
        }



        private async Task JumpTheQueueAsync(SongsItem item)
        {

            if (!await this.PlayControlFlowService.JumpTheQueueAsync(StandardAdapter(item)))
            {
                await this.ToastMessageService.ErrorAsync("操作失败");
                return;
            }

            await this.ToastMessageService.SuccessAsync("操作成功!");
        }
        private async Task LikeOrNotAsync(SongsItem item)
        {
            item.liked = await this.LikedProgressService.LikedOrNotAsync(item.id, !item.liked, CanLikedMediaType.Music) ? !item.liked : item.liked;
        }
        //private void OnSearchBoxKeyDownAsync(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs eventArgs)
        //{

        //    if (string.Equals(eventArgs.Key, "Enter", StringComparison.OrdinalIgnoreCase))
        //    {
        //        if (this._searchKeyWord?.Length > 0)
        //            this._displayTracks = this.Tracks.Where(x => x.name.Contains(this._searchKeyWord, StringComparison.OrdinalIgnoreCase)).ToArray();
        //        else if (this._displayTracks.Count != this.Tracks.Count)
        //            this._displayTracks = this.Tracks;
        //    }

        //}
        [JSInvokable]
        public void DoSearchAsync(string keyword)
        {
            if (keyword?.Length > 0)
                this._displayTracks = this.Tracks.Where(x => x.name.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToArray();
            else if (this._displayTracks.Count != this.Tracks.Count)
                this._displayTracks = this.Tracks;

            StateHasChanged();
        }
        public void Dispose()
        {
            JSRuntime.InvokeVoidAsync("eval", $"window.{r_RandomJsObjectName}=undefined");
            _objRef?.Dispose();
        }

        private static SimplePlayableItem StandardAdapter(SongsItem x)
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
    }
}
