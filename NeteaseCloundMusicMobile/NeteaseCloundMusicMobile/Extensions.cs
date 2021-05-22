using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client
{
    public static class Extensions
    {
        public static string ToCountString(this long count)
        {
            const long w1 = 10000;
            if (count <= w1)
            {
                return count.ToString();
            }
            return $"{count / w1}万";
        }
    }
}
