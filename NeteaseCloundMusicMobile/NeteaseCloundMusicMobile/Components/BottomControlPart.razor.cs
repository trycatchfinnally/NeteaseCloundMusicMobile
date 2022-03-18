using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Components
{
    public partial class BottomControlPart
    {

        private BulmaRazor.Components.Quickview _tracksQuickView;


        //private ElementReference _layoutRef;
        private ElementReference _trackQuickBodyRoot;
        private bool _opend = false;
        //private bool _openProgressing = false;
        private IDisposable _audioStateChangedSubscriber;
        private AudioPlayService AudioPlayService => PlayControlFlowService.AudioPlayService;
        [Inject]
        private IJSRuntime JS { get; set; }



        protected override Task OnInitializedAsync()
        {
            //this.AudioPlayService.AudioStateChanged += AudioPlayService_AudioStateChanged;
            if (this._audioStateChangedSubscriber == null)
                this._audioStateChangedSubscriber =
                    Observable.FromEventPattern<string>(ev => this.AudioPlayService.AudioStateChanged += ev,
                            ev => this.AudioPlayService.AudioStateChanged -= ev)
                        .Select(x => x.EventArgs)
                        .Where(x =>
                                       x == nameof(AudioPlayService.Pause)
                                    || x == nameof(AudioPlayService.PlayAsync)
                                    || x == nameof(AudioPlayService.Position))
                        .Where(x => _opend)//当关闭后，不予执行
                        .Subscribe(x => StateHasChanged());
            return base.OnInitializedAsync();
        }

        private void OnPositionSliderValueChanged(double value) =>
            AudioPlayService.Position = TimeSpan.FromSeconds(value);
        private Task HideOrShowAsync()
        {
            /*
            if (_openProgressing) return;
            _openProgressing = true;
            await JS.InvokeVoidAsync(_opend ? "hideBottom" : "showBottom", _layoutRef);
            _openProgressing = false;
            */
            _opend = !_opend;
            return Task.CompletedTask;
        }
        private void AudioPlayService_AudioStateChanged(object sender, string e)
        {

            switch (e)
            {
                case nameof(AudioPlayService.Pause):
                case nameof(AudioPlayService.PlayAsync):
                case nameof(AudioPlayService.Position):
                    StateHasChanged();
                    break;
            }
        }
        private void PlayAlbumClick()
        {
            if (this.AudioPlayService.CurrentPlayableItem != null)
                this.NavigationManager.NavigateTo("/playpanel");
        }
        private void OpenPlayList()
        {
            this._tracksQuickView.Show();

        }

        private void DeleteTrack(PlayableItemBase item)
        {
            if (AudioPlayService.CurrentPlayableItem == item)
            {
                this.ToastMessageService.ErrorAsync("歌曲正在播放，不能删除");
            }
            else
            {
                this.PlayControlFlowService.Tracks.Remove(item);
            }
        }

        private void SetVolumn(double value)
        {
            AudioPlayService.Volumn = value;
        }

        private async Task ScrollToCurrentAsync()
        {
            if (PlayControlFlowService.CurrentPlayableItem?.Id > 0)
            {

                await this.JS.InvokeVoidAsync("positionTrack", PlayControlFlowService.CurrentPlayableItem.Id, _trackQuickBodyRoot);
            }
        }
        public Task PlayAsync(PlayableItemBase item)
        {
            var result = this.PlayControlFlowService.Add2PlaySequenceAsync(item, clearCollection: false);
            if (_opend) result = Task.WhenAll(result, HideOrShowAsync());
            return result;

        }
        public async Task OnPlayOrResumeClickAsync()
        {
            if (AudioPlayService.Paused && !string.IsNullOrWhiteSpace(AudioPlayService.CurrentPlayableItem?.Url)) await AudioPlayService.PlayAsync(null);
            else AudioPlayService.Pause();
        }



        public void Dispose()
        {
            //this.AudioPlayService.AudioStateChanged -= AudioPlayService_AudioStateChanged;
            this._audioStateChangedSubscriber?.Dispose();
        }
    }
}
