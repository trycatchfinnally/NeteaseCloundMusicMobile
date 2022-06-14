using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class SimiPlaylistsApiResultModel:ApiResultModelRootBase
    {
        public List<Playlist> playlists { get; set; }
    }
}
