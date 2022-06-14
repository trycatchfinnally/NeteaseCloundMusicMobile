using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class MvUrlApiResultModel:ApiResultModelRootBase
    {
        public MvUrlData data { get; set; }
    }

    public class MvUrlData
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long r { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string md5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long expi { get; set; }
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
        public long st { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string promotionVo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }

  

}
