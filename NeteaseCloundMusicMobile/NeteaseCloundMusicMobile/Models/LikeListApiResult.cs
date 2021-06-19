using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class LikeListApiResult:ApiResultModelRootBase
    {
        public long checkPoint { get; set; }
        public List<long> ids { get; set; }
    }
}
