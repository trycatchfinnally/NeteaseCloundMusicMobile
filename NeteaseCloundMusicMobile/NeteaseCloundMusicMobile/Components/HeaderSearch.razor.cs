using Microsoft.AspNetCore.Components;

using NeteaseCloundMusicMobile.Client.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Components
{
    /// <summary>
    /// 这里独立成组件是为了避免频繁变化导致外层组件频繁刷新
    /// </summary>
    public partial class HeaderSearch
    {
        private DateTime? _lastDateTime;
        private TimeSpan _fetchFrequency = TimeSpan.FromMilliseconds(1000);
        private SearchSuggestResponseModel _searchSuggestModel;

        private static readonly Dictionary<string, KeyValuePair<string, string>> m_orderNames = new()
        {
            { "songs", KeyValuePair.Create("单曲", "fa fa-music") },
            { "albums", KeyValuePair.Create("专辑", "fa fa-music") },
            { "artists", KeyValuePair.Create("歌手", "fa fa-music") },
            { "playlists", KeyValuePair.Create("歌单", "iconfont icon-gedan") },
        };
        private async Task OnKeyWordChangedAsync(ChangeEventArgs args)
        {
            var keywords = args.Value?.ToString();
            var dtNow = DateTime.Now;
            if (!_lastDateTime.HasValue)
            {
                _lastDateTime = dtNow;
            }
            var start = _lastDateTime.Value;
            if (dtNow - start < _fetchFrequency)
            {
                _lastDateTime = dtNow;
                _searchSuggestModel = null;
                return;
            }
            _lastDateTime = dtNow;
            if (string.IsNullOrWhiteSpace(keywords)) return;
            _searchSuggestModel = await this.HttpRequestService.MakePostRequestAsync<SearchSuggestApiResultModel>("/search/suggest", new { keywords }).ContinueWith(x=>x.Result.result);

        }


    }
}
