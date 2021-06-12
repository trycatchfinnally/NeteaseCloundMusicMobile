using BulmaRazor.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Shared
{
    public partial class MainLayout
    {
        private class LoginForm
        {
            [Required(ErrorMessage ="手机号码为必填项")]
            public string Phone { get; set; }
            [Required(ErrorMessage ="密码为必填项")]
            public string PassWord { get; set; }
        }
        
        private bool _loginModalShow = false;
        private LoginForm _loginForm = new LoginForm();

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
                await provider.MarkUserAsAuthenticatedAsync(temp.profile);
                await ToastService.SuccessAsync($"登录成功！欢迎,{temp.profile.nickname}");
                this._loginModalShow = false;
            }
            else
            {
                await ToastService.ErrorAsync(temp.message);
            }

        }
        private async Task LoginOutAsync()
        {
            var provider = ApiAuthenticationStateProvider as Authentication.ApiAuthenticationStateProvider;
            await Task.WhenAll(HttpRequestService.MakePostRequestAsync("/logout"), provider.MarkUserAsLoggedOutAsync().AsTask());
        }
    }
}
