using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class Creator
    {
        /// <summary>
        /// 
        /// </summary>
        public string remarkName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mutual { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> expertTags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long djStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string followed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backgroundUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backgroundImgIdStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long avatarImgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long backgroundImgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string detailDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string defaultAvatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarImgIdStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long accountStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long vipType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long authStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userType { get; set; }
        /// <summary>
        /// 小乂怕疼
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// qq交流群:653168135
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long authority { get; set; }
    }

    public class RecommendPlaylist
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long type { get; set; }
        /// <summary>
        /// 『治愈辑』所有遗憾都是为未来的幸福做铺垫
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 为你推荐
        /// </summary>
        public string copywriter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long playcount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Creator creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alg { get; set; }
    }

    public class EvenydayRecommendPlaylistApiResult:ApiResultModelRootBase
    {
        
        /// <summary>
        /// 
        /// </summary>
        public string featureFirst { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string haveRcmdSongs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RecommendPlaylist> recommend { get; set; }
    }

}
