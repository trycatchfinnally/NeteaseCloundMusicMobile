using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> subscribers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string subscribed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string artists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TracksItem> tracks { get; set; }
        /// <summary>
        /// 刚刚更新
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
        public string opRecommend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recommendInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long adType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackNumberUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long cloudTrackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long subscribedCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string highQuality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long updateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long coverImgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string newImported { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string anonimous { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long totalDuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long specialType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string commentThreadId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long privacy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long playCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ordered { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> tags { get; set; }
        /// <summary>
        /// 云音乐中每天热度上升最快的100首单曲，每日更新。
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 飙升榜
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverImgId_str { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ToplistType { get; set; }
    }
    public class ArtistToplistArtist
    {
        public string first { get; set; }
        public string second { get; set; }
        public string third { get; set; }
    }
    public class ArtistToplist
    {

        public List<ArtistToplistArtist> artists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverUrl { get; set; }
        /// <summary>
        /// 云音乐歌手榜
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 每天更新
        /// </summary>
        public string upateFrequency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long position { get; set; }
        /// <summary>
        /// 每天更新
        /// </summary>
        public string updateFrequency { get; set; }
    }

    public class TopListApiResultModel:ApiResultModelRootBase
    {
        
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ArtistToplist artistToplist { get; set; }
    }

}
