using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class Search
    {
        /// <summary>
        /// 记录加载状态
        /// </summary>
        private bool _loading = false;
        /// <summary>
        /// 表示检索结果
        /// </summary>
        private SearchMultimatchResult _searchResponse;
        private readonly SearchRequest _searchRequest = new SearchRequest { limit = 30, type = 1 };
        private (int type, string name, string unit, RenderFragment<Search> renderFragment)[] m_supportSearchTypes =

            new[] {
            (1,"单曲" ,"首",_songsRenderFragment),
            (10,"专辑" ,"张",_albumsRenderFragment),
            (100,"歌手" ,"位",_artistsRenderFragment),
            (1000,"歌单","个",_playlistsRenderFragment ),
            (1002,"用户" ,"位",_usersRenderFragment),
            (1004,"MV" ,"首",_mvsRenderFragment),
             (1006,"歌词" ,"首",_lyricsRenderFragment),
              (1009,"电台" ,"个",_songsRenderFragment),
                (1014,"视频" ,"个",_songsRenderFragment),

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
        private class WithActionLyric : SearchLyric
        {
            private readonly Search _parent;

            public WithActionLyric(Search parent)
            {
                this._parent = parent;
            }

            public bool Expanded { get; set; } = true;

            public Task ChangeExpanded()
            {
                return _parent.OnExpandChange(this);
            }
            public Task CopyAsync()
            {
                return _parent.OnCopy(this);
            }
          
        }


        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        [Inject]
        private LikedProgressService LikedProgressService { get; set; }
        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            this._searchRequest.offset = 0;

            SearchImplAsync().ContinueWith(_ => StateHasChanged());
        }
      /// <summary>
      /// 顶部 XX首歌曲
      /// </summary>
      /// <returns></returns>
        private string MapSearchResultDescription()
        {
            var temp = m_supportSearchTypes.FirstOrDefault(x => x.type == this._searchRequest.type);
            if (temp.type == 0) temp = m_supportSearchTypes[0];

            return string.Concat(temp.unit, temp.name);
        }
    /// <summary>
    /// 获取不同模板的数量
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
        private int GetCount(int type)
        {
            var index = Array.FindIndex(m_supportSearchTypes, x => x.type == type);
            switch (index)
            {
                case 0: return this._searchResponse.songCount;
                case 1: return this._searchResponse.albumCount;
                case 2: return this._searchResponse.artistCount;
                case 3: return this._searchResponse.playlistCount;
                case 4: return this._searchResponse.userprofileCount;
                case 5: return this._searchResponse.mvCount;
                case 6: return this._searchResponse.songCount;

                default:
                    return 0;
            }
        }
        /// <summary>
        /// 根据条件取值，三元运算符
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="trueValue"></param>
        /// <param name="falseValue"></param>
        /// <returns></returns>
        private string IfElse(bool condition, string trueValue, string falseValue)
        {
           
            return condition ? trueValue : falseValue;
        }

        /// <summary>
        /// 复制歌词
        /// </summary>
        /// <param name="lyric"></param>
        /// <returns></returns>
        private async Task OnCopy(SearchLyric lyric)
        {
            if (await JSRuntime.InvokeAsync<bool>("copy", lyric.txt))
            {
                await this.ToastMessageService.SuccessAsync("复制成功");
            }
            else await this.ToastMessageService.ErrorAsync("复制失败，请稍后重试");
        }
        /// <summary>
        /// 折叠或则展开歌词
        /// </summary>
        /// <param name="lyric"></param>
        /// <returns></returns>
        private Task OnExpandChange(WithActionLyric lyric)
        {
            lyric.Expanded = !lyric.Expanded;
            StateHasChanged();
            return Task.CompletedTask;
        }
        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private Task OnPageChangeAsync(int pageIndex = 1)
        {
            this._searchRequest.offset = pageIndex * this._searchRequest.limit - this._searchRequest.limit;
            return SearchImplAsync();
        }
        /// <summary>
        /// 搜索的实现
        /// </summary>
        /// <returns></returns>
        private async Task SearchImplAsync()
        {
            _searchRequest.keywords = HttpUtility.ParseQueryString(this.NavigationManager.ToAbsoluteUri(this.NavigationManager.Uri).Query).Get("keyword");
            var typeStr = HttpUtility.ParseQueryString(this.NavigationManager.ToAbsoluteUri(this.NavigationManager.Uri).Query).Get("type");
            if (string.IsNullOrWhiteSpace(_searchRequest.keywords)) return;
            if (string.IsNullOrWhiteSpace(typeStr)) _searchRequest.type = 1;
            else if (int.TryParse(typeStr, out var type) && Array.FindIndex(m_supportSearchTypes, x => x.type == type) >= 0) _searchRequest.type = type;
            else { NotFound(); return; }
            _loading = true;
            StateHasChanged();
            _searchResponse = 
                await HttpRequestService.MakePostRequestAsync<SearchMultimatchApiResultModel>("/search", _searchRequest).ContinueWith(x => x.Result.result);
            if (_searchResponse.songs?.Length > 0&& _searchRequest.type==1006)
            {
                 
                for (int i = 0; i < _searchResponse.songs.Length; i++)
                {
                    var item = _searchResponse.songs[i];
                    if (item.lyrics == null) continue;
                    var lyric = new WithActionLyric(this) { txt = item.lyrics.txt };
                    //lyric.CopyFunction = () =>Task.CompletedTask;
                    //lyric.ChangeExpandedAction = () => Task.CompletedTask;
                    item.lyrics = lyric;
                }
            }
            _loading = false;

        }
        private async Task LikeOrNotAsync(SongsItem item)
        {
            item.liked = await this.LikedProgressService.LikedOrNotAsync(item.id, !item.liked, CanLikedMediaType.Music) ? !item.liked : item.liked;
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
