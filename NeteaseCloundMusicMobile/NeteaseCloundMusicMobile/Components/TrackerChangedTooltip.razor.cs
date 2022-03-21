using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NeteaseCloundMusicMobile.Client.Services;
using NeteaseCloundMusicMobile.Client.Shared;

namespace NeteaseCloundMusicMobile.Client.Components
{
    partial class TrackerChangedTooltip
    {
        private (bool show, bool layoutShow, string msg) _fullScreenToolTip = (false, false, String.Empty);
        private bool _shoulderRender = false;
        private IDisposable _fullScreenToolTipDisposed;
        private AudioPlayService AudioPlayService => PlayControlFlowService.AudioPlayService;
        protected override bool ShouldRender()
        {
            return _shoulderRender;
        }

        protected override Task OnInitializedAsync()
        {

            if (_fullScreenToolTipDisposed == null)
            {
                _fullScreenToolTipDisposed = Observable.FromEventPattern<string>(
                        ev => this.AudioPlayService.AudioStateChanged += ev,
                        ev => this.AudioPlayService.AudioStateChanged -= ev)
                    .Select(x => x.EventArgs)
                    .Where(x =>
                        x == nameof(AudioPlayService.Pause)
                        || x == nameof(AudioPlayService.PlayAsync))
                    .Subscribe(ProgressNext);
            }
            return base.OnInitializedAsync();
        }

        private void ProgressNext(string tracks)
        {

            if (tracks != nameof(AudioPlayService.PlayAsync)) return;
            _shoulderRender = true;
            _fullScreenToolTip.layoutShow = true;
            _fullScreenToolTip.show = true;
            _fullScreenToolTip.msg = AudioPlayService.CurrentPlayableItem.Title;
            StateHasChanged();
            
            Observable.Timer(TimeSpan.FromSeconds(0.25))
                .Do(x => _fullScreenToolTip.show = false)
                .Concat(
                    Observable.Timer(TimeSpan.FromSeconds(2))
                        .Do(x =>
                        {
                            _fullScreenToolTip.layoutShow = false;


                        })).Subscribe((_) => StateHasChanged(),()=>_shoulderRender=false);

        }

        public void Dispose()
        {
            _fullScreenToolTipDisposed?.Dispose();
        }
    }
}
