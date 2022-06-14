 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class All
    {
        /// <summary>
        /// 全部歌单
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long resourceCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long imgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long resourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hot { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string activity { get; set; }
    }

    public class SubItem
    {
        /// <summary>
        /// 综艺
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long resourceCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long imgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long resourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool hot { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string activity { get; set; }
    }



    public class PlaylistCatListApiResultModel : ApiResultModelRootBase
    {

        /// <summary>
        /// 
        /// </summary>
        public All all { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SubItem> sub { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  Dictionary<string,string> categories { get; set; }
    }

    

}
