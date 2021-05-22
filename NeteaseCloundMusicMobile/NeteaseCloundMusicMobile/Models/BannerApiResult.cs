using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class BannerApiResult: ApiResultModelRootBase
    {
        public Banner[] banners { get; set; }
    }
    public class Banner
    {
        /// <summary>
        /// 
        /// </summary>
        public string imageUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int targetId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int targetType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string titleColor { get; set; }
        /// <summary>
        /// 独家
        /// </summary>
        public string typeTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string exclusive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorImpress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorClick { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorImpressList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorClickList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorBlackList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extMonitor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extMonitorInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adLocation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adDispatchJson { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string encodeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string program { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string @event { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string video { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string song { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scm { get; set; }
    }
}
