using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class ArtistBriefInfoApiResultModel:ApiResultModelRootBase
    {
        public bool more { get; set; }
        public Artist artist { get; set; }
        public List<SongsItem> hotSongs { get; set; }
    }
}
