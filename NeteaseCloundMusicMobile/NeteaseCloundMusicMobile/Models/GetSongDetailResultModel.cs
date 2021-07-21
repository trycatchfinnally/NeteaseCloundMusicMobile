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

    public class SongsItem : TracksItem
    {

        public bool liked { get; set; }
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

    public class GetSongDetailResultModel : ApiResultModelRootBase
    {

        private List<SongsItem> _songs;
        /// <summary>
        /// 
        /// </summary>
        public List<SongsItem> songs
        {
            get
            {
                if (_songs?.Count > 0 && _songs[0].privilege == null && privileges?.Count > 0)
                {
                    _songs = _songs.Join(privileges, x => x.id, y => y.id, (x, y) =>
                        {
                            x.privilege = y;
                            return x;
                        })
                        .ToList();

                }
                return _songs;
            }
            set => _songs = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<PrivilegesItem> privileges { get; set; }

    }

}
