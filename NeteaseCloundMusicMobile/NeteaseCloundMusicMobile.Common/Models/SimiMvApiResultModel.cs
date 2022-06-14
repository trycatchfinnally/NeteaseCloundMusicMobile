using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class SimiMvApiResultModel:ApiResultModelRootBase
    {
        public List<Mv> mvs { get; set; }
    }
}
