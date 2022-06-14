 

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
        public long MvId { get; set; }

        /// <summary>
        /// 表示名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 表示是否喜欢
        /// </summary>
        public bool Liked { get; set; }

        public Album Album { get; set; }
        public IReadOnlyList<Artist> Artists { get; set; }

        /// <summary>
        /// 表示播放链接
        /// </summary>
        public string Url { get; set; }



        public long Duration { get; set; }
      
        //public abstract Task<bool> EnsureUrlAsync(IHttpRequestService httpRequestService);

        public override bool Equals(object obj)
        {
            var item = obj as PlayableItemBase;
            if (item == null) return false;
            if (this.Id != item.Id) return false;
            if (this.GetType() != item.GetType()) return false;
          
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


}
