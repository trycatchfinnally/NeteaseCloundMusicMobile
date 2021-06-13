﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NeteaseCloundMusicMobile.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class Playlist
    {

        private Models.Playlist _playlist;


        private string _searchKeyWord;


        private IReadOnlyList<TracksItem> _displayTracks = Array.Empty<TracksItem>();

        [Parameter]
        public long Id { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            var temp = await this.HttpRequestService.MakePostRequestAsync<PlaylistDetailApiResultModel>("/playlist/detail", new { id = Id });
            this._playlist = temp.playlist;
            if (this._playlist == null)
            {
                NotFound();
                return;
            }
            try
            {

                //为了避免id数量过多，分组调接口
                var groups = this._playlist.trackIds
                      .Where(x => this._playlist.tracks.All(y => y.id != x.id))
                      .Select((x, i) => KeyValuePair.Create(x.id, i))
                      .GroupBy((x) => x.Value / 30)//每组数量最多30
                      .ToDictionary(x => x.Key, x => this.HttpRequestService.MakePostRequestAsync<GetSongDetailResultModel>("/song/detail", new { ids = string.Join(",", x.Select(y => y.Key)) }));
                if (groups.Count == 0) return;
                await Task.WhenAll(groups.Values);
                var others = groups.Values.Select(x => x.Result).SelectMany(x => x.songs);
                this._playlist.tracks.AddRange(others.Select(x => new TracksItem
                {

                    id = x.id,
                    name = x.name,
                    ar = x.ar,
                    al = x.al,
                    mv = x.mv,
                    dt = x.dt

                }));
            }
            finally
            {
                this._displayTracks = this._playlist.tracks;
            }
        }
        private Task PlayAllAsync()
        {
            return this.PlayControlFlowService.AddRange2PlaySequenceAsync(this._playlist.tracks.Select(StandardAdapter), clearCollection: true);
        }

        private void OnSearchBoxKeyDownAsync(KeyboardEventArgs eventArgs)
        {
            if (string.Equals(eventArgs.Key, "Enter", StringComparison.OrdinalIgnoreCase))
            {
                if (this._searchKeyWord?.Length > 0)
                    this._displayTracks = this._playlist.tracks.Where(x => x.name.Contains(this._searchKeyWord, StringComparison.OrdinalIgnoreCase)).ToArray();
                else if (this._displayTracks.Count != this._playlist.trackCount)
                    this._displayTracks = this._playlist.tracks;
            }

        }
        private Task PlayAsync(TracksItem item)
        {
            return this.PlayControlFlowService.Add2PlaySequenceAsync(StandardAdapter(item), clearCollection: false);

        }
        private SimplePlayableItem StandardAdapter(TracksItem x)
        {
            return new SimplePlayableItem
            {

                Id = x.id,
                Title = x.name,
                Album = new Album
                {
                    id = x.al.id,
                    name = x.al.name,
                    picUrl = x.al.picUrl,

                },
                Artists = x.ar.Select(y => new Artist
                {
                    id = y.id,
                    name = y.name

                }).ToArray()
            };
        }
    }
}
