using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class ArtistMvApiResultModel : ApiResultModelRootBase
    {
        public bool hasMore { get; set; }
        public long time { get; set; }


        public List<Mv> mvs { get; set; }
    }

    public class Mv
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 超级面对面 第229期 陈奕迅：我是树上一只懒懒的豹
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 陈奕迅
        /// </summary>
        public string artistName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Artist artist { get; set; }



        public List<Artist> artists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imgurl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imgurl16v9 { get; set; }
        public string cover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long playCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string publishTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string subed { get; set; }
    }

}
