using Microsoft.JSInterop;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Services
{
    public class AudioPlayService
    {
        private readonly IJSRuntime _jSRuntime;
        private readonly IHttpRequestService _httpRequestService;

        public AudioPlayService(IJSRuntime jSRuntime,IHttpRequestService httpRequestService)
        {
            this._jSRuntime = jSRuntime;
            this._httpRequestService = httpRequestService;
        }

    }
}
