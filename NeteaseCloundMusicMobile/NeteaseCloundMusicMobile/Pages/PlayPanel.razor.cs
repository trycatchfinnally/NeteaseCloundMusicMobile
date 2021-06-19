using Microsoft.AspNetCore.Components;
using NeteaseCloundMusicMobile.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public partial class PlayPanel
    {
       
        protected override async Task OnInitializedAsync()
        {
            if (PlayControlFlowService.CurrentPlayableItem == null)
            {
                NotFound();
                return;
            }
            await base.OnInitializedAsync();
        }
    }
}
