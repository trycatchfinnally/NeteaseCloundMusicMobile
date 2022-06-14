using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{

     
    public class MvFirstApiResultItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 手术刀 Scalpel
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long playCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string briefDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string artistName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long artistId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string subed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ArtistsItem> artists { get; set; }
    }

    public class MvFirstApiResultModel:ApiResultModelRootBase
    {
        /// <summary>
        /// 
        /// </summary>
        public List<MvFirstApiResultItem> data { get; set; }
      
    }

}
