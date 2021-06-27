using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{ 
 
    public class RewardToplist
    {
        /// <summary>
        /// 
        /// </summary>
        public string coverUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SongsItem> songs { get; set; }
        /// <summary>
        /// 云音乐赞赏榜
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long position { get; set; }
    }

    public class TopListDetailApiResultModel:ApiResultModelRootBase
    {
        
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ArtistToplist artistToplist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RewardToplist rewardToplist { get; set; }
    }

}
