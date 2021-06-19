using Microsoft.AspNetCore.Components;
using NeteaseCloundMusicMobile.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Components
{
    public partial class BottomControlPart
    {


        private AudioPlayService AudioPlayService => PlayControlFlowService.AudioPlayService;

        protected override Task OnParametersSetAsync()
        {
            this.AudioPlayService.AudioStateChanged += AudioPlayService_AudioStateChanged;
            return base.OnParametersSetAsync();
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
        public async Task OnPlayOrResumeClickAsync()
        {
            if (AudioPlayService.Paused&&!string.IsNullOrWhiteSpace(AudioPlayService.CurrentPlayableItem?.Url)) await AudioPlayService.PlayAsync( null);
            else AudioPlayService.Pause();
        }

        

        public void Dispose()
        {
            this.AudioPlayService.AudioStateChanged -= AudioPlayService_AudioStateChanged;
        }
    }
}
