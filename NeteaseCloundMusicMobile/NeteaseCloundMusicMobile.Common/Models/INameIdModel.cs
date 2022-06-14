using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public interface INameIdModel
    {
        long  id { get; set; }
        string name { get; set; }
    }
}
