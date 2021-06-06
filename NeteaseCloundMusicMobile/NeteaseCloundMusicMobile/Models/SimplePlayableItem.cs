using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    /// <summary>
    /// 最干脆的东西
    /// </summary>
    public class SimplePlayableItem : PlayableItemBase
    {
        protected override async ValueTask<(string url, string picUrl, string artistName)> FetchFromApiAsync(IHttpRequestService httpRequestService)
        {
            var (item1, item2) = await TaskWhenAllHelper.WhenAllAsync(httpRequestService.MakePostRequestAsync<GetSongUrlResultModel>("/song/url", new {id= Id }, false),
                  httpRequestService.MakePostRequestAsync<GetSongDetailResultModel>("/song/detail", new { ids = Id }));
            var song = item2.songs?.FirstOrDefault() ?? new SongsItem();
            return (item1.data?.FirstOrDefault()?.url, song.al?.picUrl, string.Join(",", song.ar?.Select(x => x.name) ?? Enumerable.Empty<string>()));

        }
    }
}
