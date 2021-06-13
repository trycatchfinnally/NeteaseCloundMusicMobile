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

        [Inject]
        private AudioPlayService AudioPlayService { get; set; }

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
