using Microsoft.AspNetCore.Components;
using NeteaseCloundMusicMobile.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class MvComponent
    {

        private MvDetailApiResultModel _mvDetail;
        private MvUrlData _mvUrlData;

        private IReadOnlyList<Mv> _simiMvs;
        [Parameter]
        public long Id { get; set; }
        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    this.NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        //}

        //private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        //{
        //    Console.WriteLine(e.Location);
        //}

        protected override async Task OnParametersSetAsync()
        {
            if (Id <= 0)
            {
                NotFound(); return;
            }
            _mvDetail = null;
            _simiMvs = null;
            await base.OnParametersSetAsync();
            var postData = new { mvid = Id };
            (this._mvDetail, this._simiMvs) = await TaskWhenAllHelper.WhenAllAsync(HttpRequestService.MakePostRequestAsync<MvDetailApiResultModel>("/mv/detail", postData),
            HttpRequestService.MakePostRequestAsync<SimiMvApiResultModel>("/simi/mv", postData).ContinueWith(x => x.Result.mvs));
            if (this._mvDetail.data == null)
            {
                NotFound(); return;

            }
            if (this._mvDetail.data.brs?.Count > 0)
                await OnBrSelectedChangedAsync(this._mvDetail.data.brs[0].br);

            if (!this.PlayControlFlowService.AudioPlayService.Paused) this.PlayControlFlowService.AudioPlayService.Pause();
        }



        private async Task OnBrSelectedChangedAsync(long newValue)
        {
            var temp = await this.HttpRequestService.MakePostRequestAsync<MvUrlApiResultModel>("/mv/url", new { id = Id, r = newValue });
            this._mvUrlData = temp.data;
        }
        private void NavBack()
        {
            //this.NavigationManager.
        }
        //public void Dispose()
        //{
        //    this.NavigationManager.LocationChanged -= NavigationManager_LocationChanged;

        //}
    }
}
