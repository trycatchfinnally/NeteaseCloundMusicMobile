using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client
{
    public static class Extensions
    {

        private static readonly DateTime UnixEpoch = DateTime.UnixEpoch;
        public static string ToCountString(this long count)
        {
            const long w1 = 10000;
            if (count <= w1)
            {
                return count.ToString();
            }
            return $"{count / w1}万";
        }
        /// <summary>
        /// 将时间戳转换为时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timeStamp)
        {
          return UnixEpoch.AddMilliseconds(timeStamp);
        }

        public static async Task<bool> EnsureUrlAsync(this PlayableItemBase playableItemBase, IHttpRequestService httpRequestService)
        {
            if (playableItemBase.Id <= 0) return false;
            //if (!string.IsNullOrWhiteSpace(Url)) return true;
            var temp = await httpRequestService.MakePostRequestAsync<GetSongUrlResultModel>("/song/url", new { id = playableItemBase.Id },
                    false);
            var songUrl = temp.data?.FirstOrDefault()?.url;
            if (string.IsNullOrEmpty(songUrl))
            {

                return false;
            }
            playableItemBase.Url = songUrl;
            return true;
        }


    }
}
