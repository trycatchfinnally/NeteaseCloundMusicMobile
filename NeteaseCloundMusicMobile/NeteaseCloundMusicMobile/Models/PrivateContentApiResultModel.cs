using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class PrivateContentItem
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
        public string picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPicUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 《超级面对面》第237期  张哲瀚：不负热爱，奔赴未来
        /// </summary>
        public string copywriter { get; set; }
        /// <summary>
        /// 《超级面对面》第237期  张哲瀚：不负热爱，奔赴未来
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alg { get; set; }
    }

    public class PrivateContentApiResultModel:ApiResultModelRootBase
    {
        
        /// <summary>
        /// 独家放送
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PrivateContentItem> result { get; set; }
    }

}
