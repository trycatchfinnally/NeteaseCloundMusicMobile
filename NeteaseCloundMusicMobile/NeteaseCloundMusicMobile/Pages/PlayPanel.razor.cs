using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class PlayPanel
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        [Inject]
        private LikedProgressService LikedProgressService { get; set; }
        private PlayableItemBase CurrentTrack => PlayControlFlowService.AudioPlayService.CurrentPlayableItem;
        private IReadOnlyList<KeyValuePair<TimeSpan, string>> _lyricsKeyValuePair = Array.Empty<KeyValuePair<TimeSpan, string>>();
        private IReadOnlyList<Models.Playlist> _simiPlaylists = Array.Empty<Models.Playlist>();
        private IReadOnlyList<Models.NewSongApiResultItem> _simiSongs = Array.Empty<Models.NewSongApiResultItem>();
        private long _trackId;//用以记录当前的歌词、相似音乐等对应的歌曲id
        //private IJSObjectReference _jsModule;
        private bool _ulPanelHover = false;
        protected override async Task OnInitializedAsync()
        {
            if (PlayControlFlowService.CurrentPlayableItem == null)
            {
                NotFound();
                return;
            }
            this.PlayControlFlowService.AudioPlayService.AudioStateChanged += AudioPlayService_AudioStateChanged;

            await InitWhenTrackChangedAsync();
            await base.OnInitializedAsync();
        }
       


        private async Task InitWhenTrackChangedAsync()
        {
            if (CurrentTrack.Id == _trackId) return;
            _trackId = CurrentTrack.Id;
            var parameter = new { id = _trackId };
            var (lrcResult, simiPlaylistResult, simiSongsResult) = await TaskWhenAllHelper.WhenAllAsync(HttpRequestService.MakePostRequestAsync<LyricApiResultModel>("/lyric", parameter),
               HttpRequestService.MakePostRequestAsync<SimiPlaylistsApiResultModel>("/simi/playlist", parameter),
                 HttpRequestService.MakePostRequestAsync<SimiSongsApiResultModel>("/simi/song", parameter)
               );
            if (!string.IsNullOrWhiteSpace(lrcResult.lrc?.lyric))
            {
                var query = lrcResult.lrc.lyric.Split("\n").SelectMany(MapEachLineLyric);
                if (!string.IsNullOrWhiteSpace(lrcResult.tlyric?.lyric)) query = query.Concat(lrcResult.tlyric.lyric.Split("\n").SelectMany(MapEachLineLyric));
                this._lyricsKeyValuePair = query
                        .OrderBy(x => x.Key)
                        .ToArray();

            }
            if (simiPlaylistResult.playlists?.Count > 0) this._simiPlaylists = simiPlaylistResult.playlists;
            else this._simiPlaylists = Array.Empty<Models.Playlist>();
            if (simiSongsResult.songs?.Count > 0) this._simiSongs = simiSongsResult.songs;
            else this._simiSongs = Array.Empty<NewSongApiResultItem>();
        }
        private void LyricClick(TimeSpan lyricTime)
        {
            this.PlayControlFlowService.AudioPlayService.Position = lyricTime;
        }

        private IEnumerable<KeyValuePair<TimeSpan, string>> MapEachLineLyric(string lineForMap)
        {
            static TimeSpan ParseTimeSpan(string input)
            {

                if (TimeSpan.TryParseExact(input, @"m\:s\.fff", CultureInfo.InvariantCulture, out var ts)) return ts;
                if (TimeSpan.TryParseExact(input, @"m\:s\.ff", CultureInfo.InvariantCulture, out   ts)) return ts;
                TimeSpan.TryParseExact(input, @"m\:s", CultureInfo.InvariantCulture, out ts); return ts;
            }
            const string timeRegex = @"\[([0-9])+[0-9]:[0-5][0-9](\.([0-9])+)*\]";
            var matches = Regex.Matches(lineForMap, timeRegex);
            if (matches.Count == 0) return Enumerable.Empty<KeyValuePair<TimeSpan, string>>();
            var content = lineForMap[matches.Max(x => x.Index + x.Value.Length)..];
            return matches
                  .Select(x => x.Value[1..^1])//去除两边[和]号
                  .Select(x => KeyValuePair.Create(ParseTimeSpan(x), content));
        }
        private async void AudioPlayService_AudioStateChanged(object sender, string e)
        {

            switch (e)
            {
                case nameof(AudioPlayService.Pause):
                case nameof(AudioPlayService.PlayAsync):
                    await InitWhenTrackChangedAsync();
                    StateHasChanged();
                    break;
                case nameof(AudioPlayService.Position):
                    if (this._lyricsKeyValuePair.Count > 0)
                    {
                        var needShow = this._lyricsKeyValuePair.OrderBy(x => Math.Abs((x.Key - PlayControlFlowService.AudioPlayService.Position).TotalMilliseconds)).First();
                        await this.JSRuntime.InvokeVoidAsync("activeLi", needShow.Key.TotalMilliseconds, this._ulPanelHover);
                    }
                    break;
            }
        }
        public Task PlayAsync(Models.NewSongApiResultItem item)
        {


            var temp = new SimplePlayableItem
            {
                Id = item.id,
                Title = item.name,
                Artists = item.artists,
                Album = item.album,
                MvId = item.mvid,
                Duration=item.duration

            };

            return this.PlayControlFlowService.Add2PlaySequenceAsync(temp);
        }
        private async Task LikeOrNotAsync()
        {
            this.CurrentTrack.Liked = await this.LikedProgressService.LikedOrNotAsync(this.CurrentTrack.Id, !CurrentTrack.Liked, CanLikedMediaType.Music) ? !this.CurrentTrack.Liked : this.CurrentTrack.Liked;
        }
        public void Dispose()
        {
            this.PlayControlFlowService.AudioPlayService.AudioStateChanged -= AudioPlayService_AudioStateChanged;
        }
    }
}
