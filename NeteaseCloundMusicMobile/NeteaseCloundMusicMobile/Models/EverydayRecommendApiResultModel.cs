using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{


    public class DailySongsItem : SongsItem
    {

        /// <summary>
        /// 
        /// </summary>
        public string noCopyrightRcmd { get; set; }

        /// <summary>
        /// 根据你可能喜欢的单曲 别想他（way back home中文remix）
        /// </summary>
        public string reason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Privilege privilege { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alg { get; set; }
        public bool liked { get; set; }
    }

    public class RecommendReasonsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long songId { get; set; }
        /// <summary>
        /// 超70%人播放
        /// </summary>
        public string reason { get; set; }
    }

    public class EverydayRecommendData
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DailySongsItem> dailySongs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> orderSongs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RecommendReasonsItem> recommendReasons { get; set; }
    }

    public class EverydayRecommendApiResultModel : ApiResultModelRootBase
    {

        /// <summary>
        /// 
        /// </summary>
        public EverydayRecommendData data { get; set; }
    }

}
