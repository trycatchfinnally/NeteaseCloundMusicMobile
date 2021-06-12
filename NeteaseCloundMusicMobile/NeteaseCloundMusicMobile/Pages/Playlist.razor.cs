using Microsoft.AspNetCore.Components;
using NeteaseCloundMusicMobile.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class Playlist
    {

        private Models.Playlist _playlist;

        [Parameter]
        public long Id { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            var temp = await this.HttpRequestService.MakePostRequestAsync<PlaylistDetailApiResultModel>("/playlist/detail", new { id = Id });
            this._playlist = temp.playlist;
            if (this._playlist == null)
            {
                NotFound(); return;
            }
        }
         private void PlayAll()
        {
            
        } 
    }
}
