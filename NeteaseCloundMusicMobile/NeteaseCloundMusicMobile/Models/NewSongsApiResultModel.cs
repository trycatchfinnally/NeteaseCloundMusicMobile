using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class BMusic
    {
    }

    public class HMusic
    {
    }

    public class ArtistsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long img1v1Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long topicPerson { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string followed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> @alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long albumSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img1v1Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long picId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trans { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string briefDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long musicSize { get; set; }
        /// <summary>
        /// 颜人中
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img1v1Id_str { get; set; }
    }

    public class Artist: INameIdModel
    {
        /// <summary>
        /// 
        /// </summary>
        public long img1v1Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long topicPerson { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string followed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> @alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long albumSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img1v1Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long picId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trans { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string briefDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long musicSize { get; set; }
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
        public string img1v1Id_str { get; set; }
        public long? mvSize { get; set; }
    }

    public class Album: INameIdModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> songs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string onSale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blurPicUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long companyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pic { get; set; }
        /// <summary>
        /// 网易云音乐×云上工作室
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> @alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ArtistsItem> artists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long copyrightId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string commentThreadId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long publishTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long picId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Artist artist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string briefDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string subType { get; set; }
        /// <summary>
        /// 不舍
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picId_str { get; set; }
    }

    public class MMusic
    {
        /// <summary>
        /// 
        /// </summary>
        public long playTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long sr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long dfsId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long bitrate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long volumeDelta { get; set; }
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
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extension { get; set; }
    }

    public class LMusic
    {
        /// <summary>
        /// 
        /// </summary>
        public long playTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long sr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long dfsId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long bitrate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long volumeDelta { get; set; }
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
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extension { get; set; }
    }

    public class Privilege
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long payed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long st { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long dl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long sp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long cp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long subp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long maxbr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string toast { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string preSell { get; set; }
    }

    public class NewSongApiResultItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string starred { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long popularity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long starredNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long playedNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long dayPlays { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long hearTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mp3Url { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        public string crbt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BMusic bMusic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rtUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HMusic hMusic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> @alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Artist> artists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long copyrightId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string commentThreadId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mvid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ftype { get; set; }
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
        public Album album { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MMusic mMusic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public LMusic lMusic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string copyFrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string audition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ringtone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string disc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long position { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 不舍
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string exclusive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Privilege privilege { get; set; }
    }

    public class NewSongsApiResultModel:ApiResultModelRootBase
    {
        /// <summary>
        /// 
        /// </summary>
        public List<NewSongApiResultItem> data { get; set; }
       
    }

}
