using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using NeteaseCloundMusicMobile.Client.Utitys;

namespace NeteaseCloundMusicMobile.Client.Services
{

    /// <summary>
    /// 用来提供一些需要在所有页面进行共享的特性
    /// </summary>
    public interface IGlobalFeatureCollectionService
    {
        /// <summary>
        /// 当对应的状态发生变化的时候
        /// </summary>
        event EventHandler PlayControlFlowChanged;
        /// <summary>
        /// 表示播放控制流程
        /// </summary>
        IBasicPlayControlFlowService PlayControlFlowService { get; }

        /// <summary>
        /// 切换播放控制流程
        /// </summary>
        /// <param name="code"></param>
        void SwitchPlayControlFlow(int code);
    }

    public class GlobalFeatureCollectionService : IGlobalFeatureCollectionService
    {
        private static readonly ConcurrentDictionary<int, IBasicPlayControlFlowService> s_Cached =
            new ConcurrentDictionary<int, IBasicPlayControlFlowService>();

        private readonly IServiceProvider _serviceProvider;
        private IBasicPlayControlFlowService _playControlFlowService;

        public GlobalFeatureCollectionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            SwitchPlayControlFlow(PlayControlFlowTypeCodes.DefaultPlayControlFlowTypeCode);
        }
        public event EventHandler PlayControlFlowChanged;

        public IBasicPlayControlFlowService PlayControlFlowService
        {
            get => _playControlFlowService;
            private set
            {
                _playControlFlowService = value;
                this.PlayControlFlowChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void SwitchPlayControlFlow(int code)
        {
            if (s_Cached.TryGetValue(code, out var result))
            {
                if (this.PlayControlFlowService != result)
                    this.PlayControlFlowService = result;
                return;
            }

            Type playControlFlowService;
            //这里直接采用硬编码
            switch (code)
            {
                case PlayControlFlowTypeCodes.DefaultPlayControlFlowTypeCode:
                    playControlFlowService = typeof(DefaultPlayControlService); break;
                case PlayControlFlowTypeCodes.PersonalFmPlayControlFlowTypeCode:
                    playControlFlowService = typeof(PersonalFmPlayControlFlowService); break;
                default: throw new NotSupportedException("还未实现的播放控制流程");
            }
            result = ActivatorUtilities.CreateInstance(this._serviceProvider, playControlFlowService) as IBasicPlayControlFlowService;
            s_Cached.TryAdd(code, result);
            this.PlayControlFlowService = result;
        }
    }
}
