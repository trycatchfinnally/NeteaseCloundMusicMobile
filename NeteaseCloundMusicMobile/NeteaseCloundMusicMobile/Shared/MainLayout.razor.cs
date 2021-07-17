using BulmaRazor.Components;
using Microsoft.AspNetCore.Components;
using NeteaseCloundMusicMobile.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

          



        private void ApiAuthenticationStateProvider_AuthenticationStateChanged(Task<Microsoft.AspNetCore.Components.Authorization.AuthenticationState> task)
        {
            _ = FetchUserListAsync().ContinueWith(x => StateHasChanged());
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
        }
        protected override Task OnInitializedAsync()
        {
            this.ApiAuthenticationStateProvider.AuthenticationStateChanged += ApiAuthenticationStateProvider_AuthenticationStateChanged;
            return FetchUserListAsync();
        }
        public void Dispose()
        {
            this.ApiAuthenticationStateProvider.AuthenticationStateChanged -= ApiAuthenticationStateProvider_AuthenticationStateChanged;
        }
    }
}
