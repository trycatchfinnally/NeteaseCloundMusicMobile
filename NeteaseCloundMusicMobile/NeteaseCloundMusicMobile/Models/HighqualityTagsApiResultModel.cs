using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class PlaylistTag 
    {
        public long Id { get; set; }

        public string name { get; set; }
        public int type { get; set; }
        public int category { get; set; }

        public bool hot { get; set; }
    }
    public class HighqualityTagsApiResultModel:ApiResultModelRootBase
    {
        public List<PlaylistTag> tags { get; set; }
    }

}
