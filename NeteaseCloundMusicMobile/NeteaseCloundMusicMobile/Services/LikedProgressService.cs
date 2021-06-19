using BulmaRazor.Components;
using Microsoft.AspNetCore.Components.Authorization;
using NeteaseCloundMusicMobile.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Services
{
    /// <summary>
    /// 表示喜欢相关操作的服务
    /// </summary>
    public class LikedProgressService : IDisposable
    {
        private readonly IHttpRequestService _httpRequestService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ToastService _toastService;


        private readonly AsyncLazy<List<long>> _lazyLoadedUserLikedMusicIds;
        public LikedProgressService(IHttpRequestService httpRequestService,
            AuthenticationStateProvider authenticationStateProvider,
            ToastService toastService)
        {
            _httpRequestService = httpRequestService;
            this._authenticationStateProvider = authenticationStateProvider;
            _authenticationStateProvider.AuthenticationStateChanged += AuthenticationStateProvider_AuthenticationStateChanged;
            this._toastService = toastService;
            this._lazyLoadedUserLikedMusicIds = new AsyncLazy<List<long>>(FetchMusicLikedListAsync);

        }

        private async void AuthenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
        {

            var (value, temp) = await TaskWhenAllHelper.WhenAllAsync(this._lazyLoadedUserLikedMusicIds.EnsureValueAsync(), FetchMusicLikedListAsync());
            value.Clear();
            value.AddRange(temp);
        }


        private async Task<List<long>> FetchMusicLikedListAsync()
        {
            var status = await this._authenticationStateProvider.GetAuthenticationStateAsync();


            if (status.User.Identity.IsAuthenticated)
            {
                var uid = status.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var temp = await this._httpRequestService.MakePostRequestAsync<LikeListApiResult>("/likelist", new { uid });
                return temp.ids ?? Enumerable.Empty<long>().ToList();
            }
            return Enumerable.Empty<long>().ToList();

        }
        public async Task<bool> LikedOrNotAsync(long id, bool liked, CanLikedMediaType mediaType)
        {
            await _toastService.SuccessAsync("操作成功！");
            return true;
        }
        public async Task<bool> DetermineMusicLikedStatusAsync(long musicId)
        {
            if (musicId <= 0) return false;
            var value = await this._lazyLoadedUserLikedMusicIds.EnsureValueAsync();
            return value.Contains(musicId);

        }
        public Task<List<long>>EnsureLikedMusicIdsAsync()=> this._lazyLoadedUserLikedMusicIds.EnsureValueAsync();
        public void Dispose()
        {
            this._authenticationStateProvider.AuthenticationStateChanged -= AuthenticationStateProvider_AuthenticationStateChanged;
        }
    }



    public enum CanLikedMediaType
    {
        Comment, Music
    }
}
