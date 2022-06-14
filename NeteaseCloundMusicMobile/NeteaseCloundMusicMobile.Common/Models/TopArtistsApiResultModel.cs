using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class TopArtistsListItem
    {
        public List<TopArtistsItem> artists { get; set; }
        public long updateTime { get; set; }
        public int type { get; set; }
    }
    public class TopArtistsItem:Artist
    {
        public long score { get; set; }
        /// <summary>
        /// 上回排名
        /// </summary>
        public int lastRank { get; set; }
    }
    public class TopArtistsApiResultModel:ApiResultModelRootBase
    {
        public TopArtistsListItem list { get; set; }
    }


}
