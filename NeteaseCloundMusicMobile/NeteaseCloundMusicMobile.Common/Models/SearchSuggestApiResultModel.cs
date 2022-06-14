using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class SearchSuggestApiResultModel : ApiResultModelRootBase
    {
        public SearchSuggestResponseModel result { get; set; }
    }

    public class SearchSuggestResponseModel
    {
        public string[] order { get; set; }
        public Album[] albums { get; set; }
        public Artist[] artists { get; set; }
        public Playlist[] playlists { get; set; }
        public SuggestSongsItem[] songs { get; set; }

        public Array this[string key]
        {
            get
            {
                //var prop=  typeof(SearchSuggestResponseModel)
                //      .GetProperty(key, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                //  if (prop != null) return prop.GetValue(this) as Array;
                //  return null;
                //这里用switch更好
                switch (key)
                {
                    case nameof(albums): return albums;
                    case nameof(songs): return songs;
                    case nameof(artists): return artists;
                    case nameof(playlists): return playlists;
                    default: throw new IndexOutOfRangeException(nameof(key));
                }

            }
        }
    }


    public class SuggestSongsItem : SongsItem
    {
        public Artist[] artists { get; set; }
    }

}
