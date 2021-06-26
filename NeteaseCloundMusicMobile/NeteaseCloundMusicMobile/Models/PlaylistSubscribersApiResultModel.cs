using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    //public class SubscribersItem
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string defaultAvatar { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int province { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int authStatus { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string followed { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string avatarUrl { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int accountStatus { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int gender { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int city { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int birthday { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int userId { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int userType { get; set; }
    //    /// <summary>
    //    /// 爱学习的小可爱_Love
    //    /// </summary>
    //    public string nickname { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string signature { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string description { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string detailDescription { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int avatarImgId { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int backgroundImgId { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string backgroundUrl { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int authority { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string mutual { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string expertTags { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string experts { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int djStatus { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int vipType { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string remarkName { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int subscribeTime { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string backgroundImgIdStr { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string avatarImgIdStr { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string vipRights { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string avatarImgId_str { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string avatarDetail { get; set; }
    //}

    public class PlaylistSubscribersApiResultModel:ApiResultModelRootBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string more { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SubscribersItem> subscribers { get; set; }
    }

}
