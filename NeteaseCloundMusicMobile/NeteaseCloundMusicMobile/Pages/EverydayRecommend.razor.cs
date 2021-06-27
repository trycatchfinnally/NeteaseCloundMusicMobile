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
        private Components.PlaylistTable _playlistTable;
       
        
        private Task PlayAllAsync()
        {
            return _playlistTable.PlayAllAsync();
        }
      
       
      
       
        protected override async Task OnInitializedAsync()
        {

            var temp = await this.HttpRequestService.MakePostRequestAsync<EverydayRecommendApiResultModel>("/recommend/songs");
            if (temp.data?.dailySongs?.Count > 0)
            {
              
                this._dailySongs = temp.data.dailySongs;
               
            }
            else
            {
                this._dailySongs = Array.Empty<DailySongsItem>();
                
            }
            await base.OnInitializedAsync();
        }
    }
}
