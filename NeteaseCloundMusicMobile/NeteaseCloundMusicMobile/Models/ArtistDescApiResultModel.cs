using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class IntroductionItem
    {
        /// <summary>
        /// 代表作品
        /// </summary>
        public string ti { get; set; }
        /// <summary>
        /// 我的快乐时代、K歌之王、十年、富士山下、好久不见、阴天快乐、最佳损友、浮夸、与我常在、爱情转移、你的背包、你给我听好、人来人往、不要说话、单车、苦瓜、可以了、陪你度过漫长岁月、我们
        /// </summary>
        public string txt { get; set; }
    }

    public class ContentItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 本文来源于微信号“港乐清单”（ID：hkmusicmovie），作者港乐君
        /// </summary>
        public string content { get; set; }
    }

    public class Topic
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long addTime { get; set; }
        /// <summary>
        /// 「林夕＋陈奕迅＋泽日生」系列之二：明明千丝万缕，偏说一丝不挂
        /// </summary>
        public string mainTitle { get; set; }
        /// <summary>
        /// 「林夕＋陈奕迅＋泽日生」系列之二：明明千丝万缕，偏说一丝不挂
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ContentItem> content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long cover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long headPic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shareContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wxTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showComment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long seriesId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pubTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long readCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> tags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pubImmidiatly { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string auditor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long auditTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long auditStatus { get; set; }
        /// <summary>
        /// 本文来源于微信号“港乐清单”（ID：hkmusicmovie），作者港乐君 接上一篇，毋庸置疑，林夕和泽日生的《富士山下》把陈奕迅的事业推向了另外一个高度，获得了无数奖项。所以2010年，在陈奕迅推出
        /// </summary>
        public string startText { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string delReason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showRelated { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fromBackend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long rectanglePic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long updateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reward { get; set; }
        /// <summary>
        /// 明明千丝万缕，偏说一丝不挂。
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long categoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long hotScore { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recomdTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recomdContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long number { get; set; }
    }




    public class TopicDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public Topic topic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Creator creator { get; set; }
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
        public long likedCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string liked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long rewardCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long rewardMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string relatedResource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rectanglePicUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long categoryId { get; set; }
        /// <summary>
        /// 音乐心情
        /// </summary>
        public string categoryName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string commentThreadId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showComment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showRelated { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 明明千丝万缕，偏说一丝不挂。
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 「林夕＋陈奕迅＋泽日生」系列之二：明明千丝万缕，偏说一丝不挂
        /// </summary>
        public string recmdTitle { get; set; }
        /// <summary>
        /// 明明千丝万缕，偏说一丝不挂。
        /// </summary>
        public string recmdContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reward { get; set; }
        /// <summary>
        /// 「林夕＋陈奕迅＋泽日生」系列之二：明明千丝万缕，偏说一丝不挂
        /// </summary>
        public string mainTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long seriesId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shareContent { get; set; }
        /// <summary>
        /// 「林夕＋陈奕迅＋泽日生」系列之二：明明千丝万缕，偏说一丝不挂
        /// </summary>
        public string wxTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long addTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> tags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long readCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 「林夕＋陈奕迅＋泽日生」系列之二：明明千丝万缕，偏说一丝不挂
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long number { get; set; }
    }

    public class ArtistDescApiResultModel:ApiResultModelRootBase
    {
        /// <summary>
        /// 
        /// </summary>
        public List<IntroductionItem> introduction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string briefDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TopicDataItem> topicData { get; set; }
    
    }

}
