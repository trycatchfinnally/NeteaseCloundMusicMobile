using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class SearchMultimatchApiResultModel:ApiResultModelRootBase
    {
        public SearchMultimatchResult result { get; set; }
    }

    public class SearchMultimatchResult
    {
        public bool hasMore { get; set; }
        public string[]  queryCorrected { get; set; }
        public int songCount { get; set; }

        public SearchSongsItem[] songs { get; set; }


        public int albumCount { get; set; }

        public Album[] albums { get; set; }


        public int artistCount { get; set; }
        public List<SearchArtistItem> artists { get; set; }



        public int playlistCount { get; set; }

        public List<Playlist> playlists { get; set; }

        public int userprofileCount { get; set; }
        public List<UserProfile> userprofiles { get; set; }


        public int mvCount { get; set; }
        public List<Mv> mvs { get; set; }




    }
    public class SearchArtistItem : Artist { public long accountId { get; set; }}
    public  class SearchSongsItem : SongsItem
    {
        public List<ArItem> artists { get=>ar; set=>ar=value; }

        public Al album { get => al; set => al = value; }

        public long duration { get => dt; set => dt = value; }
        public SearchLyric lyrics { get; set; }
    }

    public class SearchLyric
    {
        public string txt { get; set; }
    }
}
