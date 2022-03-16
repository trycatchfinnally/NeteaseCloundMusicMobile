using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Components
{
    public partial class BottomControlPart
    {

        private BulmaRazor.Components.Quickview _tracksQuickView;


        private ElementReference _layoutRef;
        private ElementReference _trackQuickBodyRoot;
        private bool _opend = true;
        private bool _openProgressing = false;
        private AudioPlayService AudioPlayService => PlayControlFlowService.AudioPlayService;
        [Inject]
        private IJSRuntime JS { get; set; }

       

        protected override Task OnInitializedAsync()
        {
            this.AudioPlayService.AudioStateChanged += AudioPlayService_AudioStateChanged;
           
            return base.OnParametersSetAsync();
        }

        private async Task HideOrShowAsync()
        {
            
            if (_openProgressing) return;
            _openProgressing = true;
            await JS.InvokeVoidAsync(_opend ? "hideBottom" : "showBottom", _layoutRef);
            _openProgressing = false;
            _opend = !_opend;
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
            this.AudioPlayService.AudioStateChanged -= AudioPlayService_AudioStateChanged;
        }
    }
}
