using Microsoft.AspNetCore.Components;

using NeteaseCloundMusicMobile.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BulmaRazor.Components;
using Microsoft.Extensions.DependencyInjection;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Utitys;

namespace NeteaseCloundMusicMobile.Client.Shared
{
    /// <summary>
    /// 用作组件的基类，以便注入一些通用的服务
    /// </summary>
    public abstract class RazorComponentBase : ComponentBase, IAsyncDisposable
    {
        private IDisposable _subscribePlayControlFlow;
        [Inject]
        protected IHttpRequestService HttpRequestService { get; set; }

        [Inject]
        protected ToastService ToastMessageService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IGlobalFeatureCollectionService GlobalFeatureCollectionService { get; set; }


        protected IPlayControlFlowService PlayControlFlowService =>
            GlobalFeatureCollectionService.PlayControlFlowService;

        /// <summary>
        /// 是否订阅属性改变事件，注意，如果该项为true，则在IPlayControlFlowService发生变化的时候，会刷新页面
        /// </summary>
        protected virtual bool SubscribePlayControlFlowChanged => false;


        /// <summary>
        /// 子类重写异步方法
        /// </summary>
        protected sealed override void OnInitialized()
        {
            base.OnInitialized();
            if (SubscribePlayControlFlowChanged && this._subscribePlayControlFlow == null)
            {
                this._subscribePlayControlFlow = Observable.FromEventPattern(
                        x => GlobalFeatureCollectionService.PlayControlFlowChanged += x,
                        x => GlobalFeatureCollectionService.PlayControlFlowChanged -= x)
                    .Subscribe(_ => StateHasChanged());
            }
        }
        /// <summary>
        /// 使用默认的播放控制流程播放
        /// </summary>
        /// <param name="playableItem"></param>
        /// <param name="autoPlay"></param>
        /// <param name="clearCollection"></param>
        /// <returns></returns>
        protected Task Add2PlaySequenceAsync(PlayableItemBase playableItem, bool autoPlay = true,
            bool clearCollection = false)
        {
            if (!this.PlayControlFlowService.SupportPlayTrack)
                this.GlobalFeatureCollectionService.SwitchPlayControlFlow(PlayControlFlowTypeCodes.DefaultPlayControlFlowTypeCode);
            return this.PlayControlFlowService.Add2PlaySequenceAsync(playableItem, autoPlay, clearCollection);
        }

        protected Task AddRange2PlaySequenceAsync(IEnumerable<PlayableItemBase> playableItems, bool autoPlay = true,
            bool clearCollection = false)
        {
            if (!this.PlayControlFlowService.SupportPlayTrack)
                this.GlobalFeatureCollectionService.SwitchPlayControlFlow(PlayControlFlowTypeCodes.DefaultPlayControlFlowTypeCode);
            return this.PlayControlFlowService.AddRange2PlaySequenceAsync(playableItems, autoPlay, clearCollection);
        }
        /// <summary>
        /// 在一系列值中选取第一个不为空的字符串
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        protected string Nvl(params string[] lists)
        {

            return lists.FirstOrDefault(static x => !string.IsNullOrWhiteSpace(x));
        }
        protected void NotFound()
        {
            this.NavigationManager.NavigateTo("/404");
        }
        /// <summary>
        /// NOTE:Components shouldn't need to implement IDisposable and IAsyncDisposable simultaneously. If both are implemented, the framework only executes the asynchronous overload.
        ///使用 异步实现调用子类可能的Dispose方法，以便不更改代码的方式释放<see cref="IGlobalFeatureCollectionService"/>PlayControlFlowChanged事件的订阅
        /// 
        /// </summary>
        /// <returns></returns>
        public ValueTask DisposeAsync()
        {

            if (this._subscribePlayControlFlow != null) this._subscribePlayControlFlow.Dispose();


            if (this is IDisposable disposable)
            {

                disposable.Dispose();
            }
            return ValueTask.CompletedTask;
        }


    }
}
