using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Components
{
    public partial class Empty
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

    }
}
