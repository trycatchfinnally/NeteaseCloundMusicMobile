using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client
{
    public static class DebugJsonWriter
    {
        public static void WriteLine(string msg)
        {
#if DEBUG
            Console.WriteLine(msg);
#endif
        }
        public static void WriteLine(object msg)
        {
            WriteLine(msg?.ToString());
        }
        public static void WriteJson(object data)
        {
            if (data != null)
                WriteLine(System.Text.Json.JsonSerializer.Serialize(data));
            else WriteLine(string.Empty);
        }
    }
}
