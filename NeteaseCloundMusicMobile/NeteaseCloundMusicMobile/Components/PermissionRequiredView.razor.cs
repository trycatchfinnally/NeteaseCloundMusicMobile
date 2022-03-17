using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace NeteaseCloundMusicMobile.Client.Components
{
    partial class PermissionRequiredView
    {
        /// <summary>
        /// 对应子元素
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        /// <summary>
        /// 当发生点击行为的时候，需要执行的事件
        /// </summary>
        [Parameter]
        public EventCallback OnClick { get; set; }
        /// <summary>
        /// 如果设置该值，表示直接跳转
        /// </summary>
        [Parameter]
        public string Href { get; set; }

        /// <summary>
        /// 弹出登录框
        /// </summary>
        [CascadingParameter(Name = nameof(ShowLoginModalAction))]
        public Action ShowLoginModalAction { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]

        private  NavigationManager NavigationManager { get; set; }
        private async Task OnClickAsync()
        {
            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
           
            if (state.User.Identity?.IsAuthenticated == true && OnClick.HasDelegate)

            {
                await OnClick.InvokeAsync();
                return;
            }
            ShowLoginModalAction?.Invoke();

        }

        private async Task OnHrefClickAsync()
        {
            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (state.User.Identity?.IsAuthenticated == true  )
            {
                NavigationManager.NavigateTo(Href);
                return;
            }
            ShowLoginModalAction?.Invoke();
        }
        protected override bool ShouldRender()
        {
            return false; 
        }
    }
}
