using Microsoft.AspNetCore.Components;
using NeteaseCloundMusicMobile.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class TopArtists
    {

        private IReadOnlyList<TopArtistsItem> _topArtists;


        private static readonly Dictionary<int, string> m_supportTypes = new Dictionary<int, string> {
            { 1, "华语" }, { 2, "欧美" }, { 3, "韩国" }, { 4, "日本" }
        };
        [Parameter]
        public int Type { get; set; }
        //protected override async Task OnInitializedAsync()
        //{
        //    var topArtists = await Task.WhenAll(Enumerable.Range(1, 4).Select(x => HttpRequestService.MakePostRequestAsync<TopArtistsApiResultModel>("/toplist/artist", new { type = x })));
        //    this._topArtists = topArtists.GroupBy(x => x.list.type).ToDictionary(x => x.Key, x => x.SelectMany (y => y.list.artists).ToArray());
        //    await base.OnInitializedAsync();
        //}
        protected override async Task OnParametersSetAsync()
        {
            this._topArtists = null;
            await base.OnParametersSetAsync();
            if (!m_supportTypes.ContainsKey(Type))
            {
                NotFound(); return;
            }
            var temp = await HttpRequestService.MakePostRequestAsync<TopArtistsApiResultModel>("/toplist/artist", new { type = Type });
            this._topArtists = temp.list.artists;
        }
    }
}
