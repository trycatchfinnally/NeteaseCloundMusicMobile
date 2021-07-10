using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class HighqualityPlaylistApiResultModel:ApiResultModelRootBase
    {
        public bool more { get; set; }
        public long lasttime { get; set; }
        public int total { get; set; }
        public List<Playlist> playlists { get; set; }
    }
}
