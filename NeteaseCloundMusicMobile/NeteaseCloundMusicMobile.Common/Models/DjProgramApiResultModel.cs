using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    
    
    public class MainSong
    {
        /// <summary>
        /// 我承认参与过制造女性焦虑 | 伊能静
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long position { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> @alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long copyrightId { get; set; }
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
        public List<ArtistsItem> artists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Album album { get; set; }
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
        public long score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long starredNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long duration { get; set; }
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
        public string ringtone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string crbt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string audition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string copyFrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string commentThreadId { get; set; }
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
        public long copyright { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string transName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long originCoverType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string originSongSimpleData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long single { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string noCopyrightRcmd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BMusic bMusic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mp3Url { get; set; }
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
        public long mvid { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        public LMusic lMusic { get; set; }
    }

    public class Dj
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
        /// 新周刊FM
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 「新周刊」官方账号
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 「新周刊」官方账号
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
        
        //public string expertTags { get; set; }
        
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
        public string backgroundImgIdStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarImgIdStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string anchor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarImgId_str { get; set; }
        /// <summary>
        /// 硬核读书会FM
        /// </summary>
        public string brand { get; set; }
    }

    public class Radio
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dj { get; set; }
        /// <summary>
        /// 硬核读书会FM
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
       
    public string desc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long subCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long programCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long categoryId { get; set; }
        /// <summary>
        /// 侃侃而谈
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long radioFeeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long feeScope { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string buyed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string videos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string finished { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string underShelf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long purchaseCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long originalPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string discountPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long lastProgramCreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lastProgramName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long lastProgramId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long picId { get; set; }
        /// <summary>
        /// 「新周刊·硬核读书会」出品的文化访谈播客
        /// </summary>
        public string rcmdText { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hightQuality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string whiteList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string composeVideo { get; set; }
    }

    public class DjProgram
    {
        /// <summary>
        /// 
        /// </summary>
        public MainSong mainSong { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string songs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dj dj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blurCoverUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Radio radio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long subscribedCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reward { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long malongrackId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long serialNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long listenerCount { get; set; }
        /// <summary>
        /// 我承认参与过制造女性焦虑 | 伊能静
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long createTime { get; set; }

        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string commentThreadId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> channels { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string titbits { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string titbitImages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pubStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long bdAuditStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long programFeeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string buyed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string programDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> h5Links { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long coverId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long adjustedPlayCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string canReward { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long auditStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string publish { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isPublish { get; set; }
    }

    public class DjProgramApiResultItem
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
        /// 我承认参与过制造女性焦虑 | 伊能静
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 编辑推荐：本期嘉宾伊能静，听听她一路走来的所思所想
        /// </summary>
        public string copywriter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string canDislike { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trackNumberUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DjProgram program { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alg { get; set; }
    }

    public class DjProgramApiResultModel : ApiResultModelRootBase
    {

        /// <summary>
        /// 
        /// </summary>
        public long category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DjProgramApiResultItem> result { get; set; }
    }

}
