using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Authentication
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {

        private class LoginStatusApiResultModel : ApiResultModelRootBase
        {
            public LoginApiResultModel data { get; set; }
           
        }

        private readonly IHttpRequestService _httpRequestService;



        private static readonly ClaimsPrincipal s_empty_principal = new ClaimsPrincipal(new ClaimsIdentity());
    
        public ApiAuthenticationStateProvider(IHttpRequestService httpRequestService)
        {

            _httpRequestService = httpRequestService;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //var savedToken = await this._localStorageService.GetItemAsStringAsync(UserProfile);

            //if (string.IsNullOrWhiteSpace(savedToken))
            //{
            //    return new AuthenticationState(s_empty_principal);
            //}
            try
            {
                //var claims = ParseClaimsFromUserProfile(JsonSerializer.Deserialize<UserProfile>(ParseBase64WithoutPadding(savedToken)));

                var temp = await this._httpRequestService.MakePostRequestAsync<LoginStatusApiResultModel>("/login/status");
                if (temp.data?.profile?.userId > 0)
                {
                    var claims = ParseClaimsFromUserProfile(temp.data.profile);


                    var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                   
                    return new AuthenticationState(authenticatedUser);

                }

                return new AuthenticationState(s_empty_principal);
            }
            catch 
            {


                return new AuthenticationState(s_empty_principal);
            }

        }
        /// <summary>
        /// 根据指定的token，让用户登录状态
        /// </summary>
        /// <param name="token"></param>
        public void MarkUserAsAuthenticated(UserProfile user)
        {
            var claims = ParseClaimsFromUserProfile(user);


            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
           
         
            NotifyAuthenticationStateChanged(authState);
        }
        /// <summary>
        ///通知退出登录
        /// </summary>
        public async Task MarkUserAsLoggedOutAsync()
        {
            var anonymousUser = s_empty_principal;
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            await _httpRequestService.MakePostRequestAsync("/logout");
            NotifyAuthenticationStateChanged(authState);
        }
        private static IEnumerable<Claim> ParseClaimsFromUserProfile(UserProfile user)
        {
            return new[] {
            new Claim(ClaimTypes.Name,user.nickname),
            new Claim(ClaimTypes.NameIdentifier,user.userId.ToString()),
            new Claim(ClaimTypes.Uri,user.avatarUrl)
            };
        }
        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var temp = jwt.Split('.');
            if (temp.Length != 3)
            {

                return Enumerable.Empty<Claim>();
            }
            var payload = temp[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            if (keyValuePairs == null)
            {
                return Enumerable.Empty<Claim>();
            }



            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value?.ToString() ?? String.Empty)));

            return claims;
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}
