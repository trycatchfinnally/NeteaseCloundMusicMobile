using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Components
{
    public partial class PlaylistTagButton
    {
        [Parameter]
        public bool Checked { get; set; }
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public string MoreText { get; set; }

    }
}
