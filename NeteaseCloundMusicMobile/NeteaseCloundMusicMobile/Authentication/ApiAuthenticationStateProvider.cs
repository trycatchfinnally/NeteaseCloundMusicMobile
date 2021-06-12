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
        private readonly ILocalStorageService _localStorageService;
     


        private static readonly ClaimsPrincipal s_empty_principal = new ClaimsPrincipal(new ClaimsIdentity());
        private const string UserProfile = nameof(UserProfile);
        public ApiAuthenticationStateProvider(ILocalStorageService localStorageService )
        {
            _localStorageService = localStorageService;  
        }

       
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await this._localStorageService.GetItemAsStringAsync(UserProfile);

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(s_empty_principal);
            }
            try
            {
                var claims = ParseClaimsFromUserProfile(JsonSerializer.Deserialize<UserProfile>(ParseBase64WithoutPadding(savedToken)));


                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                await this._localStorageService.RemoveItemAsync(UserProfile);
                return new AuthenticationState(s_empty_principal);
            }

        }
        /// <summary>
        /// 根据指定的token，让用户登录状态
        /// </summary>
        /// <param name="token"></param>
        public async ValueTask MarkUserAsAuthenticatedAsync(UserProfile user)
        {
            var claims = ParseClaimsFromUserProfile(user);


            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            var utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(new { user.nickname, user.userId, user.avatarUrl });
            var base64String = Convert.ToBase64String(utf8Bytes);
          
            await this._localStorageService.SetItemAsStringAsync(UserProfile, base64String);
            NotifyAuthenticationStateChanged(authState);
        }
        /// <summary>
        ///通知退出登录
        /// </summary>
        public async ValueTask MarkUserAsLoggedOutAsync()
        {
            var anonymousUser = s_empty_principal;
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            await this._localStorageService.RemoveItemAsync(UserProfile);
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
