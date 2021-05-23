using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    /// <summary>
    /// 表示可播放的音乐项
    /// </summary>
    public abstract class PlayableItemBase
    {
        /// <summary>
        /// 对应项目的id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 表示图片地址
        /// </summary>
        public string PicUrl { get; private set; }
        /// <summary>
        /// 表示名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 表示歌手名
        /// </summary>
        public string ArtistName { get; private set; }
        /// <summary>
        /// 表示播放链接
        /// </summary>
        public string Url { get; private set; }

        public async Task EnsureUrlAsync(IHttpRequestService httpRequestService, bool forceRefresh = false)
        {
            if (forceRefresh || string.IsNullOrEmpty(Url))
            {
                (Url, PicUrl, ArtistName) = await FetchFromApiAsync(httpRequestService);
            }
        }
        protected abstract ValueTask<(string url, string picUrl, string artistName)> FetchFromApiAsync(IHttpRequestService httpRequestService);
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return (obj as PlayableItemBase).Id == Id;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


}
