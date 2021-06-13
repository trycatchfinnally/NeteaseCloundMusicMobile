using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client
{
    public static class Extensions
    {

        private static readonly DateTime s_begin_datetime=new DateTime(1970, 1, 1);
        public static string ToCountString(this long count)
        {
            const long w1 = 10000;
            if (count <= w1)
            {
                return count.ToString();
            }
            return $"{count / w1}万";
        }

        public static DateTime ToDateTime(this long timeStamp)
        {
          return s_begin_datetime.AddMilliseconds(timeStamp);
        }


       
    }
}
