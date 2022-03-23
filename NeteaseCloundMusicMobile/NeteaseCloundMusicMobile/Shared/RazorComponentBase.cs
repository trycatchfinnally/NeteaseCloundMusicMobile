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
    /// ��������Ļ��࣬�Ա�ע��һЩͨ�õķ���
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
        /// �Ƿ������Ըı��¼���ע�⣬�������Ϊtrue������IPlayControlFlowService�����仯��ʱ�򣬻�ˢ��ҳ��
        /// </summary>
        protected virtual bool SubscribePlayControlFlowChanged => false;


        /// <summary>
        /// ������д�첽����
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
        /// ʹ��Ĭ�ϵĲ��ſ������̲���
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
        /// ��һϵ��ֵ��ѡȡ��һ����Ϊ�յ��ַ���
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
        ///ʹ�� �첽ʵ�ֵ���������ܵ�Dispose�������Ա㲻���Ĵ���ķ�ʽ�ͷ�<see cref="IGlobalFeatureCollectionService"/>PlayControlFlowChanged�¼��Ķ���
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
