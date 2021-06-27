using Microsoft.AspNetCore.Components;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class ArtistComponent
    {

        private class ArtistAlbumQuery
        {
            public int limit { get; set; }
            public int offset { get; set; }
            public long id { get; set; }
        }

        private ArtistBriefInfoApiResultModel _briefInfo;
        private IReadOnlyList<IntroductionItem> _introductions;
        private ArtistAlbumApiResultModel _artistAlbumApiResultModel;
        private ArtistAlbumQuery _artistAlbumQuery;
        private bool _artistAlbumLoading = false;
        [Parameter]
        public long Id { get; set; }
        [Inject]
        private LikedProgressService LikedProgressService { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            if (Id <= 0)
            {
                NotFound();
                return;
            }
            _briefInfo = null;
            await base.OnParametersSetAsync();
            var postData = new { id = Id };
            this._artistAlbumQuery = new ArtistAlbumQuery
            {
                limit = 30,
                offset = 0,
                id = Id
            };
            var (briefInfo, introductionsResult, _) = await TaskWhenAllHelper.WhenAllAsync(HttpRequestService.MakePostRequestAsync<ArtistBriefInfoApiResultModel>("/artists", postData),
                HttpRequestService.MakePostRequestAsync<ArtistDescApiResultModel>("/artist/desc", postData),
               FetchArtistAlbumAsync()
                );
            if (briefInfo.artist == null)
            {
                NotFound();
                return;
            }
            this._briefInfo = briefInfo;
            this._introductions = introductionsResult.introduction;
        }
        private async Task<bool> FetchArtistAlbumAsync()
        {
            if (_artistAlbumLoading) return _artistAlbumLoading;
            _artistAlbumLoading = true;
            var temp = await HttpRequestService.MakePostRequestAsync<ArtistAlbumApiResultModel>("/artist/album", this._artistAlbumQuery);
            if (temp.hotAlbums == null)
            {
                _artistAlbumLoading = false;
                return _artistAlbumLoading;
            }
            var oldData = (this._artistAlbumApiResultModel?.hotAlbums as IReadOnlyList<Album>) ?? Array.Empty<Album>();
            temp.hotAlbums.InsertRange(0, oldData);
            this._artistAlbumApiResultModel = temp;
            this._artistAlbumQuery.offset += this._artistAlbumApiResultModel.hotAlbums.Count;
            _artistAlbumLoading = false;

            return _artistAlbumLoading;
        }
    }
}
