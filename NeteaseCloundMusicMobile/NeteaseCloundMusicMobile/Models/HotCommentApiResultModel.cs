using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class Associator
    {
        /// <summary>
        /// 
        /// </summary>
        public int vipCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rights { get; set; }
    }

    public class VipRights
    {
        /// <summary>
        /// 
        /// </summary>
        public Associator associator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string musicPackage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int redVipAnnualCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int redVipLevel { get; set; }
    }

 

    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public string locationInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string liveInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long anonym { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long vipType { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        public AvatarDetail avatarDetail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string followed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mutual { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remarkName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public VipRights vipRights { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nickname { get; set; }
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
        public string expertTags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string experts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CommonIdentity commonIdentity { get; set; }
    }

    public class Decoration
    {
    }

    public class  CommentsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public User user { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CommentsItem> beReplied { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PendantData pendantData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ShowFloorComment showFloorComment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long commentId { get; set; }
        /// <summary>
        /// 我还以为是谁的歌单这么有品位。原来是我自己哦
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long likedCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string expressionUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long commentLocationType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long parentCommentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decoration decoration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string repliedMark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string liked { get; set; }
    }

    public class HotCommentApiResultModel:ApiResultModelRootBase
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CommentsItem> topComments { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hasMore { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CommentsItem> hotComments { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long total { get; set; }
       
    }

}
