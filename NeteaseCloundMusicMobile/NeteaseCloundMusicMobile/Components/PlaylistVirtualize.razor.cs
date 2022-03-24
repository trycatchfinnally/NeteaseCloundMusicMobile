using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;

namespace NeteaseCloundMusicMobile.Client.Components
{
    partial class PlaylistVirtualize
    {
        private ICollection<SongsItem> _displayTracks = Array.Empty<SongsItem>();

        private Dictionary<SongsItem, bool>  _checkedSongsInfoDictionary;
        private DotNetObjectReference<PlaylistVirtualize> _objRef;
        private ElementReference _searchElement;
        //定义一个随机名，用以创建随机的js对象名
        private readonly string r_RandomJsObjectName = Path.GetRandomFileName().Substring(0, 8);


        /// <summary>
        /// 播放的项目
        /// </summary>
        [Parameter]
        public ICollection<SongsItem> Tracks { get; set; }
        /// <summary>
        /// 是否支持检索
        /// </summary>
        [Parameter]

        public bool EnableSearch { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        [Inject]
        private LikedProgressService LikedProgressService { get; set; }

        private bool AllChecked => _displayTracks.All(x => _checkedSongsInfoDictionary[x]);

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && EnableSearch)
            {
                _objRef = DotNetObjectReference.Create(this);
                await this.JSRuntime.InvokeVoidAsync("eval", $"window.{r_RandomJsObjectName}=new searchProgress()");
                await this.JSRuntime.InvokeVoidAsync($"{r_RandomJsObjectName}.initComponent", _objRef, this._searchElement);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Tracks == null) throw new NotSupportedException();
            await base.OnParametersSetAsync();
        
            var likedIds = await this.LikedProgressService.EnsureLikedMusicIdsAsync();
            foreach (var item in Tracks)
            {
                item.liked = likedIds.Contains(item.id);
            }

            this._displayTracks = Tracks;
            _checkedSongsInfoDictionary = Tracks.ToDictionary(x => x, x => false);
        }

        public Task PlayAllAsync()
        {

            return this.AddRange2PlaySequenceAsync(Tracks.Select(StandardAdapter), clearCollection: true);
        }
        public Task PlayAsync(SongsItem item)
        {

            return this.Add2PlaySequenceAsync(StandardAdapter(item), clearCollection: false);

        }
        public Task PlaySelectedAsync()
        {
            //var rows = this._dtPlaylist.GetCheckedItems();
            //if (rows.Count == 0) return ToastMessageService.ErrorAsync("请先选择项").AsTask();

            //return this.AddRange2PlaySequenceAsync(rows.Select(StandardAdapter), clearCollection: true);

            var items = this._checkedSongsInfoDictionary
                .Where(x => x.Value)
                .Select(x => x.Key)
                .Select(StandardAdapter)
                .ToArray();
            if (items.Length == 0) return ToastMessageService.ErrorAsync("请先选择项").AsTask();

            return this.AddRange2PlaySequenceAsync(items, clearCollection: true);

        }


        private void AllCheckedChanged(bool value)
        {
            foreach (var key in this._checkedSongsInfoDictionary.Keys)
            {
                _checkedSongsInfoDictionary[key] = value;
            }
        }
        private Task SkipTryAsync()
        {

            var temp = this.Tracks.Where(x => x.privilege == null || x.privilege.fl * x.privilege.pl > 0)
                .Select(StandardAdapter);
            return this.AddRange2PlaySequenceAsync(temp, clearCollection: true);
        }
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
