using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class SimiSongsApiResultModel:ApiResultModelRootBase
    {
        public List<NewSongApiResultItem> songs { get; set; }
    }
}
