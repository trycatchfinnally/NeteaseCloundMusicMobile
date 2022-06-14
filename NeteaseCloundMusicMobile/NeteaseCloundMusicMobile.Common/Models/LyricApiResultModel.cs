using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class Lrc
    {
        /// <summary>
        /// 
        /// </summary>
        public int version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lyric { get; set; }
    }

    public class Klyric
    {
        /// <summary>
        /// 
        /// </summary>
        public int version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lyric { get; set; }
    }

    public class Tlyric
    {
        /// <summary>
        /// 
        /// </summary>
        public int version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lyric { get; set; }
    }

    public class LyricApiResultModel:ApiResultModelRootBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string sgc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string qfy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Lrc lrc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Klyric klyric { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Tlyric tlyric { get; set; }
        
    }

}
