using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class SubscribersItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string defaultAvatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long authStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string followed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long accountStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string detailDescription { get; set; }
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
        public string backgroundUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long authority { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mutual { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string expertTags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string experts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long djStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long vipType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remarkName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long authenticationTypes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarDetail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string anchor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backgroundImgIdStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarImgIdStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarImgId_str { get; set; }
    }
 
    public class TracksItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pst { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long t { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ArItem> ar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> alia { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pop { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long st { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long v { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string crbt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Al al { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long dt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public H h { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public M m { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public L l { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string a { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rtUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ftype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> rtUrls { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long djId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long copyright { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long s_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long originCoverType { get; set; }
        /*{
    "originSongSimpleData": {
        "songId": 1472921626,
        "name": "刻在我心底的名字",
        "artists": [
            {
                "id": 3690,
                "name": "卢广仲"
            }
        ],
        "albumMeta": {
            "id": 94304499,
            "name": "刻在我心底的名字"
        }
    }
}*/
        //public string originSongSimpleData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long single { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string noCopyrightRcmd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long cp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long rtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rurl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mst { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mv { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long publishTime { get; set; }
    }

    public class TrackIdsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long v { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long t { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long at { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long uid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rcmdReason { get; set; }
    }

    public class Playlist
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// binaryify喜欢的音乐
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long coverImgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverImgId_str { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long adType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string opRecommend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string highQuality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string newImported { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long updateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long specialType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long privacy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string commentThreadId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long playCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackNumberUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long subscribedCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long cloudTrackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ordered { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> tags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updateFrequency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long backgroundCoverId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backgroundCoverUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long titleImage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string titleImageUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string englishTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string officialPlaylistType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SubscribersItem> subscribers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string subscribed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Creator creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TracksItem> tracks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string videoIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string videos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TrackIdsItem> trackIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long shareCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long commentCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remixVideo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sharedUsers { get; set; }
    }

 
    public class PlaylistDetailApiResultModel:ApiResultModelRootBase
    {
      
        /// <summary>
        /// 
        /// </summary>
        public string relatedVideos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Playlist playlist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string urls { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PrivilegesItem> privileges { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sharedPrivilege { get; set; }
    }

}
