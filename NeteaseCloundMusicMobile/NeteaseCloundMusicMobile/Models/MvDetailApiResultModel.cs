using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class Mp
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mvFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long payed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long dl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long cp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long sid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long st { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string normal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unauthorized { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }

    public class BrsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long br { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long polong { get; set; }
    }
 
    public class VideoGroupItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long type { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 广岛之恋
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long artistId { get; set; }
        /// <summary>
        /// 莫文蔚
        /// </summary>
        public string artistName { get; set; }
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
        public string cover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverId_str { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long coverId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long playCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long subCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long shareCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long commentCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long nType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string publishTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<BrsItem> brs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ArtistsItem> artists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string commentThreadId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<VideoGroupItem> videoGroup { get; set; }
    }

    public class MvDetailApiResultModel:ApiResultModelRootBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string loadingPic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bufferPic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string loadingPicFS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bufferPicFS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string subed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Mp mp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
     
    }

}
