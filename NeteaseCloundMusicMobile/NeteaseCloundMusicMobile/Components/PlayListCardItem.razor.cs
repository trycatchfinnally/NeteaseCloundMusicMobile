using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Components
{
    public partial class PlayListCardItem
    {
        [Parameter]
        public string PicUrl { get; set; }
        [Parameter]

        public long Count { get; set; }
        [Parameter]
        public string CountIconClass { get; set; }
        [Parameter]

        public string FooterTitle { get; set; }
        [Parameter]

        public string FooterDescription { get; set; }
        protected override bool ShouldRender()
        {
            return false;
        }
    }
}
