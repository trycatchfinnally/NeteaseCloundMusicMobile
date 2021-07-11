using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class UserPlaylistApiResultModel:ApiResultModelRootBase
    {
        public bool more { get; set; }
        public string version { get; set; }
        public List<Playlist> playlist { get; set; }
    }
}
