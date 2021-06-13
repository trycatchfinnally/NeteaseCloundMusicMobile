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


        public override async Task<bool> EnsureUrlAsync(IHttpRequestService httpRequestService)
        {
            if (Id <= 0) return false;
            //if (!string.IsNullOrWhiteSpace(Url)) return true;
            var temp = await httpRequestService.MakePostRequestAsync<GetSongUrlResultModel>("/song/url", new { id = Id },
                    false);
            var songUrl = temp.data?.FirstOrDefault()?.url;
            if (string.IsNullOrEmpty(songUrl))
            {

                return false;
            }
            this.Url = songUrl;
            return true;
        }
    }
}
