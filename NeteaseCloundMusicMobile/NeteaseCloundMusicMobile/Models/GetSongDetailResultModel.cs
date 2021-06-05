using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class ArItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> tns { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> @alias { get; set; }
    }

    public class Al
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 海阔天空
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> tns { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pic_str { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pic { get; set; }
    }

    public class H
    {
        /// <summary>
        /// 
        /// </summary>
        public long br { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long vd { get; set; }
    }

    public class M
    {
        /// <summary>
        /// 
        /// </summary>
        public long br { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long vd { get; set; }
    }

    public class L
    {
        /// <summary>
        /// 
        /// </summary>
        public long br { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long vd { get; set; }
    }

    public class SongsItem
    {
        /// <summary>
        /// 海阔天空
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
        /// <summary>
        /// 
        /// </summary>
        public string originSongSimpleData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resourceState { get; set; }
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
        public long mv { get; set; }
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
        public long cp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long publishTime { get; set; }
    }

    //public class FreeTrialPrivilege
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string resConsumable { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string userConsumable { get; set; }
    //}

    public class ChargeInfoListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long rate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string chargeUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string chargeMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long chargeType { get; set; }
    }

    public class PrivilegesItem
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
        /// <summary>
        /// 
        /// </summary>
        public long playMaxbr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long downloadMaxbr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rscl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FreeTrialPrivilege freeTrialPrivilege { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ChargeInfoListItem> chargeInfoList { get; set; }
    }

    public class GetSongDetailResultModel:ApiResultModelRootBase
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SongsItem> songs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PrivilegesItem> privileges { get; set; }
  
    }

}
