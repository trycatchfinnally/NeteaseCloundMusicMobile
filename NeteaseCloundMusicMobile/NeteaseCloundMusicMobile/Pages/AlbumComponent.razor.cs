using Microsoft.AspNetCore.Components;
using NeteaseCloundMusicMobile.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class AlbumComponent
    {

        private AlbumDetailApiResultModel _albumDetailApiResultModel;
        private Components.PlaylistVirtualize _playlistTable;

        [Parameter]
        public long Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Id <= 0)
            {
                NotFound();
                return;
            }
            _albumDetailApiResultModel = null;
            this._albumDetailApiResultModel = await this.HttpRequestService.MakePostRequestAsync<AlbumDetailApiResultModel>("/album", new { id = Id });
            if (_albumDetailApiResultModel?.album == null)
            {
                NotFound();
                return;
            }
            await base.OnParametersSetAsync();
        }
        private Task PlayAllAsync() => _playlistTable.PlayAllAsync();
    }
}
