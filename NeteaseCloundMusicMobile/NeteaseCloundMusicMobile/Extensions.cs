using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client
{
    public static class Extensions
    {

        private static readonly DateTime UnixEpoch = DateTime.UnixEpoch;
        public static string ToCountString(this long count)
        {
            const long w1 = 10000;
            if (count <= w1)
            {
                return count.ToString();
            }
            return $"{count / w1}万";
        }
        /// <summary>
        /// 将时间戳转换为时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timeStamp)
        {
          return UnixEpoch.AddMilliseconds(timeStamp);
        }


       
    }
}
