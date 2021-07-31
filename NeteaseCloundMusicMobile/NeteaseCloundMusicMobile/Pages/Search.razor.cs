using Microsoft.AspNetCore.Components;

using NeteaseCloundMusicMobile.Client.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class Search
    {
        private bool _loading = false;

        private SearchMultimatchResult _searchResponse;
        private readonly SearchRequest _searchRequest = new SearchRequest { limit = 30, type = 1 };
        private     (int type, string name, string unit, RenderFragment<Search> renderFragment)[] m_supportSearchTypes = 
            
            new[] {
            (1,"单曲" ,"首",_songsRenderFragment),
            (10,"专辑" ,"张",_albumsRenderFragment),
            (100,"歌手" ,"位",_songsRenderFragment),
            (1000,"歌单","个",_songsRenderFragment ),
            (1002,"用户" ,"位",_songsRenderFragment),
            (1004,"MV" ,"首",_songsRenderFragment),
             (1006,"歌词" ,"首",_songsRenderFragment),
              (1009,"电台" ,"个",_songsRenderFragment),
                (1014,"视频" ,"个",_songsRenderFragment),
            (1018,"综合" ,"首",_songsRenderFragment),
        };
        private class SearchRequest
        {
            public int limit { get; set; }
            public int offset { get; set; }
            public string keywords { get; set; }
            /// <summary>
            ///  搜索类型；默认为 1 即单曲 , 取值意义 : 1: 单曲, 10: 专辑, 100: 歌手, 1000:
            ///歌单, 1002: 用户, 1004: MV, 1006: 歌词, 1009: 电台, 1014: 视频, 1018:综合
            /// </summary>
            public int type { get; set; }
        }
         
        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            this._searchRequest.offset = 0;
            
            SearchImplAsync().ContinueWith(_ => StateHasChanged());
        }
        private string MapSearchResultDescription()
        {
            var temp = m_supportSearchTypes.FirstOrDefault(x => x.type == this._searchRequest.type);
            if (temp.type == 0) temp = m_supportSearchTypes[0];

            return string.Concat(temp.unit,temp.name);
        }
        private int GetCount(int type)
        {
            switch (type)
            {
                case 1:return this._searchResponse.songCount;
                case 10: return this._searchResponse.albumCount;

                default:
                    return 0;
            }
        }
      
        private Task OnPageChangeAsync(int pageIndex=1)
        {
            this._searchRequest.offset = pageIndex * this._searchRequest.limit - this._searchRequest.limit;
            return SearchImplAsync();
        }
        private async Task SearchImplAsync()
        {
            _searchRequest.keywords = HttpUtility.ParseQueryString(this.NavigationManager.ToAbsoluteUri(this.NavigationManager.Uri).Query).Get("keyword");
            var typeStr = HttpUtility.ParseQueryString(this.NavigationManager.ToAbsoluteUri(this.NavigationManager.Uri).Query).Get("type");
            if (string.IsNullOrWhiteSpace(_searchRequest.keywords)) return;
            if (string.IsNullOrWhiteSpace(typeStr)) _searchRequest.type = 1;
            else if (int.TryParse(typeStr, out var type)) _searchRequest.type = type;
            else { NotFound(); return; }
            _loading = true;
            StateHasChanged();
            _searchResponse = await HttpRequestService.MakePostRequestAsync<SearchMultimatchApiResultModel>("/search", _searchRequest).ContinueWith(x => x.Result.result);

            _loading = false;

        }
        protected override Task OnInitializedAsync()
        {
            this.NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            return SearchImplAsync();
        }

        public void Dispose()
        {
            this.NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
        }
    }
}
