using BulmaRazor.Components;
using Microsoft.AspNetCore.Components;
using NeteaseCloundMusicMobile.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NeteaseCloundMusicMobile.Client.Services;
using NeteaseCloundMusicMobile.Client.Utitys;
using Index = NeteaseCloundMusicMobile.Client.Pages.Index;

namespace NeteaseCloundMusicMobile.Client.Shared
{
    public partial class MainLayout
    {
        private class LoginForm
        {
            [Required(ErrorMessage = "手机号码为必填项")]
            public string Phone { get; set; }
            [Required(ErrorMessage = "密码为必填项")]
            public string PassWord { get; set; }
        }




        private bool _loginModalShow = false;
        private Quickview _userQuickview;
        private readonly LoginForm _loginForm = new LoginForm();
        private IReadOnlyList<Playlist> _userPlaylist = Array.Empty<Playlist>();

        private Action _showLoginModalAction;
        [Inject]
        private IGlobalFeatureCollectionService GlobalFeatureCollectionService { get; set; }
        [Inject]
        private ILogger<MainLayout> Logger { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private void ShowUserQuickView() => _userQuickview.Show();
        private async Task LoginAsync()
        {
            var temp = await HttpRequestService.MakePostRequestAsync<Models.LoginApiResultModel>("/login/cellphone", new
            {
                phone = this._loginForm.Phone,
                md5_password = Md5Helper.GetHashString(this._loginForm.PassWord).ToLower()
            });
            if (temp.code == 200)
            {
                var provider = ApiAuthenticationStateProvider as Authentication.ApiAuthenticationStateProvider;
                provider.MarkUserAsAuthenticated(temp.profile);

                await ToastService.SuccessAsync($"登录成功！欢迎,{temp.profile.nickname}");
                this._loginModalShow = false;
            }
            else
            {
                await ToastService.ErrorAsync(temp.message);
            }

        }
        private Task LoginOutAsync()
        {
            var provider = ApiAuthenticationStateProvider as Authentication.ApiAuthenticationStateProvider;
            return provider.MarkUserAsLoggedOutAsync();
        }



        private void ShowLoginModal()
        {
            this._loginModalShow = true;
            StateHasChanged();
        }

        private void ApiAuthenticationStateProvider_AuthenticationStateChanged(Task<Microsoft.AspNetCore.Components.Authorization.AuthenticationState> task)
        {
            FetchUserListAsync().ToObservable().Subscribe();
        }
        private async Task PersonalFmClickAsync()
        {

            this.GlobalFeatureCollectionService.SwitchPlayControlFlow(PlayControlFlowTypeCodes.PersonalFmPlayControlFlowTypeCode);
            var retryCount = 0;
            while (retryCount++ < 10)
            {
                if (!GlobalFeatureCollectionService.PlayControlFlowService.AudioPlayService.Paused)
                {
                    NavigationManager.NavigateTo("/playpanel"); return;
                }
                await Task.Delay(100).ConfigureAwait(false);

            }
            this.Logger.LogWarning("自动切换到播放页面失败");
        }

        private async Task FetchUserListAsync()
        {
            var state = await ApiAuthenticationStateProvider.GetAuthenticationStateAsync();
            if (state.User.Identity.IsAuthenticated)
            {
                var temp = await this.HttpRequestService.MakePostRequestAsync<UserPlaylistApiResultModel>("/user/playlist", new { uid = state.User.FindFirst(ClaimTypes.NameIdentifier).Value }); ;
                this._userPlaylist = temp.playlist;
            }
            else this._userPlaylist = Array.Empty<Playlist>();

            StateHasChanged();
        }

        protected override Task OnInitializedAsync()
        {
            this.ApiAuthenticationStateProvider.AuthenticationStateChanged += ApiAuthenticationStateProvider_AuthenticationStateChanged;
            _showLoginModalAction = ShowLoginModal;
            return FetchUserListAsync();
        }
        public void Dispose()
        {
            this.ApiAuthenticationStateProvider.AuthenticationStateChanged -= ApiAuthenticationStateProvider_AuthenticationStateChanged;
        }
    }
}
