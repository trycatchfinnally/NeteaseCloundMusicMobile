using Microsoft.AspNetCore.Components;

using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Pages
{
    public abstract class RazorPageBase:ComponentBase
    {
        [Inject]
        protected IHttpRequestService HttpRequestService { get; set; }
    }
}
