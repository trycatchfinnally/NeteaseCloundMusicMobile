using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class SimiArtistApiResultModel:ApiResultModelRootBase
    {
        public List<Artist> artists { get; set; }
    }
}
