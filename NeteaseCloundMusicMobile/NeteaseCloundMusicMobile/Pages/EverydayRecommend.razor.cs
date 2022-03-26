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
        private ICollection<SongsItem> _dailySongs;
        private Components.PlaylistVirtualize _playlistTable;
       
        
        private Task PlayAllAsync()
        {
            return _playlistTable.PlayAllAsync();
        }
      
       
      
       
        protected override async Task OnInitializedAsync()
        {

            var temp = await this.HttpRequestService.MakePostRequestAsync<EverydayRecommendApiResultModel>("/recommend/songs").ConfigureAwait(false);
            if (temp.data?.dailySongs?.Count > 0)
            {
              
                this._dailySongs = temp.data.dailySongs.Select(x=>x as SongsItem).ToArray();
               
            }
            else
            {
                this._dailySongs = Array.Empty<SongsItem>();
                
            }
            await base.OnInitializedAsync();
        }
    }
}
