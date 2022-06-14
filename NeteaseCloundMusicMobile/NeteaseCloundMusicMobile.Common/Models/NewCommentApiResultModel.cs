using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{

   
    public class CommonIdentity
    {
        public string title { get; set; }
        public string iconUrl { get; set; }
        public string link { get; set; }
        public string target { get; set; }
        public string bizCode { get; set; }

    }
    public class PendantData
    {
        public long id { get; set; }
        public string imageUrl { get; set; }
    }
    public class AvatarDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public long userType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long identityLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string identityIconUrl { get; set; }
    }

    

    public class ShowFloorComment
    {
        /// <summary>
        /// 
        /// </summary>
        public long replyCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string comments { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showReplyCount { get; set; }
        ///// <summary>
        ///// "[5340972278]"
        ///// </summary>
        //public string topCommentIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string target { get; set; }
    }

    

    public class Tag
    {
        /// <summary>
        /// 
        /// </summary>
        public string datas { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string relatedCommentIds { get; set; }
    }

    public class ExtInfo
    {
    }

    public class CommentVideoVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string showCreationEntrance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string allowCreation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string creationOrpheusUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string playOrpheusUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long videoCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string forbidCreationText { get; set; }
    }


    public class SortTypeListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long sortType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sortTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string target { get; set; }
    }

    public class NewCommentData
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CommentsItem> comments { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string currentComment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long totalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hasMore { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cursor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long sortType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SortTypeListItem> sortTypeList { get; set; }
    }

    public class NewCommentApiResultModel : ApiResultModelRootBase
    {

        /// <summary>
        /// 
        /// </summary>
        public NewCommentData data { get; set; }
    }

}
