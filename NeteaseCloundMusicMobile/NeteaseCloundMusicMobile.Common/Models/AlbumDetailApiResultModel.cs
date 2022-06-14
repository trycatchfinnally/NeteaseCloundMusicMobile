using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class AlbumDetailApiResultModel:ApiResultModelRootBase
    {
        public bool resourceState { get; set; }
        public List<SongsItem> songs { get; set; }

        public AlbumDetail album { get; set; }
    }
    public class AlbumDetail : Album
    {
        public Info info { get; set; }
    }

    public class ResourceInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 神的游戏
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string encodedId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string subTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string webUrl { get; set; }
    }

    public class CommentThread
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ResourceInfo resourceInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long resourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long commentCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long likedCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long shareCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long hotCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserProfile[] latestLikedUsers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long resourceOwnerId { get; set; }
        /// <summary>
        /// 神的游戏
        /// </summary>
        public string resourceTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long resourceId { get; set; }
    }

    public class Info
    {
        /// <summary>
        /// 
        /// </summary>
        public CommentThread commentThread { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserProfile[] latestLikedUsers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string liked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string comments { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long resourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long resourceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long commentCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long likedCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long shareCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string threadId { get; set; }
    }

    
}
