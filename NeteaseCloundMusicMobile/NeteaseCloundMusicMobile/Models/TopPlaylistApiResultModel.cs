using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class TopPlaylistApiResultModel: ApiResultModelRootBase
    {
        public int total { get; set; }
        public bool more { get; set; }
        public string cat { get; set; }
        public List<Playlist> Playlists { get; set; }
    }
}
