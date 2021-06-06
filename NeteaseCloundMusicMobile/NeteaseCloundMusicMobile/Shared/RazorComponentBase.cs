using Microsoft.AspNetCore.Components;

using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Shared
{
    public abstract class RazorComponentBase:ComponentBase
    {
        [Inject]
        protected IHttpRequestService HttpRequestService { get; set; }


        [Inject]
        protected AudioPlayService AudioPlayService { get; set; }
    }
}
