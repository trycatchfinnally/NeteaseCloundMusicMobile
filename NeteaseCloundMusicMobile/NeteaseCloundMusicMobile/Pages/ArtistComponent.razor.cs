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
        private IReadOnlyList<Artist> _simiArtists;
        private ArtistAlbumApiResultModel _artistAlbumApiResultModel;
        private ArtistAlbumQuery _artistAlbumQuery;
        private bool _artistAlbumLoading = false;


        private ArtistMvApiResultModel _artistMvApiResultModel;
        private ArtistAlbumQuery _artistMvQuery;
        private bool _artistMvLoading = false;

        [Parameter]
        public long Id { get; set; }
       
        protected override async Task OnParametersSetAsync()
        {
            if (Id <= 0)
            {
                NotFound();
                return;
            }
            //全部复位
            _briefInfo = null;
            _introductions = null;
            _simiArtists = null;
            _artistAlbumApiResultModel = null;
            _artistAlbumLoading = false;
            _artistMvApiResultModel = null;
            _artistMvLoading = false;
            await base.OnParametersSetAsync();
            var postData = new { id = Id };
            this._artistAlbumQuery = new ArtistAlbumQuery
            {
                limit = 30,
                offset = 0,
                id = Id
            };
            this._artistMvQuery = new ArtistAlbumQuery
            {
                limit = 30,
                offset = 0,
                id = Id
            };
            var (briefInfo, introductionsResult, _, _, simiArtistApiResult) = await TaskWhenAllHelper.WhenAllAsync(HttpRequestService.MakePostRequestAsync<ArtistBriefInfoApiResultModel>("/artists", postData),
                HttpRequestService.MakePostRequestAsync<ArtistDescApiResultModel>("/artist/desc", postData),
               FetchArtistAlbumAsync(), FetchArtistMvAsync(),
               HttpRequestService.MakePostRequestAsync<SimiArtistApiResultModel>("/simi/artist", postData)
                );
            if (briefInfo.artist == null)
            {
                NotFound();
                return;
            }
            this._briefInfo = briefInfo;
            this._introductions = introductionsResult.introduction;
            this._simiArtists = simiArtistApiResult.artists;
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


        private async Task<bool> FetchArtistMvAsync()
        {
            if (_artistMvLoading) return _artistMvLoading;
            _artistMvLoading = true;
            var temp = await HttpRequestService.MakePostRequestAsync<ArtistMvApiResultModel>("/artist/mv", this._artistMvQuery);
            if (temp.mvs == null)
            {
                _artistMvLoading = false;
                return _artistMvLoading;
            }
            var oldData = (this._artistMvApiResultModel?.mvs as IReadOnlyList<Mv>) ?? Array.Empty<Mv>();
            temp.mvs.InsertRange(0, oldData);
            this._artistMvApiResultModel = temp;
            this._artistMvQuery.offset += this._artistMvApiResultModel.mvs.Count;
            _artistMvLoading = false;

            return _artistMvLoading;
        }
    }
}
